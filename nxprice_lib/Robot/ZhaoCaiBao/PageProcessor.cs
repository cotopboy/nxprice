using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using HtmlAgilityPack;
using System.Threading;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;


namespace nxprice_lib.Robot.ZhaoCaiBao
{
    public class PageProcessor
    {
        private static object retListSyncObj = new object();

        private int pageIndex = 0;
        private TargetRecord jobInfo;
        private PageLoader pageLoader;
        private FileDb db;
        private MaxiPageMgr maxiPageMgr;

        private List<ZCBRecord> RecordList = new List<ZCBRecord>();

        private List<ZCBRecord> retRecordListRef;

        public PageProcessor(int pageIndex,
                             TargetRecord jobInfo,
                             PageLoader pageLoader,
                             FileDb db,
                             MaxiPageMgr maxiPageMgr)
        {
            this.maxiPageMgr = maxiPageMgr;
            this.db = db;
            this.pageLoader = pageLoader;
            this.pageIndex = pageIndex;
            this.jobInfo = jobInfo;
        }

        private ManualResetEvent eventHandle = new ManualResetEvent(false);

        public ManualResetEvent EventHandle
        {
            get { return eventHandle; }
        } 


        public void GetPageRecord()
        {

            if (maxiPageMgr.MaxPageCount.HasValue && maxiPageMgr.MaxPageCount.Value < this.pageIndex)
            {
                this.EventHandle.Set();
                return;
            }

            string pageUrl = this.jobInfo.Url.Replace("#pageNum#", pageIndex.ToString());
            string pageHtml = this.pageLoader.GetPageHtml(pageUrl, false,
                                                          this.db.WebProxy.UserName,
                                                          this.db.WebProxy.Password,
                                                          Encoding.GetEncoding("gb2312")
                                                         );

            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);



            try
            {
                string maxiPageCountContent = doc.DocumentNode
                                    .SelectSingleNode("//*[@class='ui-paging-info']")
                                    .SelectSingleNode(".//*[@class='ui-paging-bold']").InnerText;

                int maxPageCount = maxiPageCountContent.SubStringAfter("/", 1).StrToInt();

                maxiPageMgr.MaxPageCount = maxPageCount;
            }
            catch { }

            var nodes = doc.DocumentNode.Descendants("ul")
                         .Where(
                                d => d.Attributes.Contains("class")
                                && d.Attributes["class"].Value.Contains("icontent-ul")
                               )
                         .ToList();

            foreach (var targetNode in nodes)
            {
                CheckSinleNode(targetNode);
            }

            lock (retListSyncObj)
            {
                this.retRecordListRef.AddRange(RecordList);
            }

            this.EventHandle.Set();

        }

        private void CheckSinleNode(HtmlNode targetNode)
        {
            string yearRate = targetNode.SelectSingleNode(".//*[contains(@class,'f-18')]").InnerText;

            string dayLeft = GetCleanText(targetNode.SelectSingleNode(".//*[@class='year']").InnerText).Substring(0, 4);

            string minMount = GetCleanText(targetNode.SelectSingleNode(".//*[@class='w154']").InnerText).Substring(0, 3);

            string dealCount = GetCleanText(targetNode.SelectSingleNode(".//*[@class='w123']").InnerText).Substring(0, 1);

            string productionId = targetNode.Attributes["productid"].Value;

            var record = new ZCBRecord(yearRate, dayLeft, minMount, this.pageIndex, dealCount, productionId);

            RecordList.Add(record);

            Console.WriteLine("Page={0:D2}     Rate={1}        DayLeft={2}     MinMount={3}        BuyIndex={4:F1}              prouctionId={5}",
                              this.pageIndex, yearRate, dayLeft, minMount, record.BuyIndex, record.ProductionID);
        }

        private string GetCleanText(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Trim();
        }

        internal void SetReturnListRef(List<ZCBRecord> RecordList)
        {
            retRecordListRef = RecordList;
        }
    }
}
