using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Threading;
using HtmlAgilityPack;
using System.Globalization;
using System.IO;
using Utilities.IO;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Serialization;


namespace nxprice_lib.Robot
{
    public class ZhaoCaiBaoRobot :Robot,IRobot
    {
        private static volatile bool isBusy = false;

        private static List<ZCBRecord> RecordList = new List<ZCBRecord>();
        private int currenPageIndex = 0;

        private ZcbFileDb zcbFileDb = null;


        public ZhaoCaiBaoRobot(FileDb db) : base(db)
        {
            
        }

        public override void DoJob(TargetRecord jobInfo)
        {
            this.jobInfo = jobInfo;

            if (isBusy) return;
            isBusy = true;


            this.zcbFileDb = new FileDbEngine<ZcbFileDb>("ZcbDb", ".xml").LoadFileDB();

            Thread thread = new Thread(new ThreadStart(DoJobInternal));
            thread.IsBackground = true;
            thread.Start();


        }

        private void DoJobInternal()
        {
            RecordList = new List<ZCBRecord>();

            for (int i = 1; i <= this.zcbFileDb.MaxPageCount; i++)
            {
                this.currenPageIndex = i;
                CheckSinglePage(i);
            }

            DoAnalysis();

            isBusy = false;
        }

        private void DoAnalysis()
        {
            
            var HittedRecord = RecordList.Where(x => x.MinMount >= this.zcbFileDb.MinMountLowerLimit
                                                    && x.DayLeft <= this.zcbFileDb.DayLeftUpperLimit
                                                    && x.DealCount <= this.zcbFileDb.DealCountUpperLimit)
                                         .OrderByDescending(x=>x.BuyIndex)
                                         .Take(this.zcbFileDb.DisplayTopCount)
                                         .ToList();

            Console.ForegroundColor = ConsoleColor.Green;
            
            foreach (var item in HittedRecord)
            {

                Console.WriteLine("Page={0:D2}     Rate={1}        DayLeft={2}     MinMount={3}        BuyIndex={4:F1}",
                                  item.PageIndex, item.YearRate, item.DayLeft, item.MinMount, item.BuyIndex);
      
            }

            Console.ResetColor();

            double average = RecordList.Average(x => x.BuyIndex);

            Console.ForegroundColor = ConsoleColor.Green;

            Console.WriteLine("Average={0:F3}",average);

            Console.ResetColor();

            var first = HittedRecord.FirstOrDefault();

            if (first != null)
            {
                SaveHistory(first);
            }

            if (first.BuyIndex > this.zcbFileDb.BuyIndexLimit)
            {
                ExportToBuyUi(first);
            }
        }

        private void ExportToBuyUi(ZCBRecord first)
        {
            string dirExchange = DirectoryHelper.CombineWithCurrentExeDir("ZcbExchange");
            if (!Directory.Exists(dirExchange)) Directory.CreateDirectory(dirExchange);

            string filePath = Path.Combine(dirExchange, first.ProductionID);

            FileInfo file = new FileInfo(filePath);

            if(!file.Exists) file.Create();


        }

        private void SaveHistory(ZCBRecord first)
        {
            FileInfo zcbHistoryFile = new FileInfo (DirectoryHelper.CombineWithCurrentExeDir("ZcbHistory.csv"));
            zcbHistoryFile.Append(
                                    DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss").AddQuotes()
                                  + "," 
                                  + first.BuyIndex.ToString(CultureInfo.InvariantCulture).AddQuotes()
                                  + Environment.NewLine
                                  , Encoding.UTF8
                                  );       
        }

        private void CheckSinglePage(int pageIndex)
        {
            string pageUrl = this.jobInfo.Url.Replace("#pageNum#", pageIndex.ToString());
            string pageHtml = this.GetPageHtml(pageUrl,true,Encoding.GetEncoding("gb2312"));

            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);

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
        }

        private void CheckSinleNode(HtmlNode targetNode)
        {
            string yearRate = targetNode.SelectSingleNode(".//*[contains(@class,'f-18')]").InnerText;

            string dayLeft = GetCleanText(targetNode.SelectSingleNode(".//*[@class='year']").InnerText).Substring(0, 4);

            string minMount = GetCleanText(targetNode.SelectSingleNode(".//*[@class='w154']").InnerText).Substring(0,3);

            string dealCount = GetCleanText(targetNode.SelectSingleNode(".//*[@class='w123']").InnerText).Substring(0, 1);

            string productionId = targetNode.Attributes["productid"].Value;

            var record = new ZCBRecord(yearRate, dayLeft, minMount, currenPageIndex, dealCount, productionId);

            RecordList.Add(record);

            Console.WriteLine("Page={0:D2}     Rate={1}        DayLeft={2}     MinMount={3}        BuyIndex={4:F1}              prouctionId={5}",
                              this.currenPageIndex,yearRate,dayLeft,minMount,record.BuyIndex,record.ProductionID);
        }

        private string GetCleanText(string input)
        {
            return input.Replace("\n", "").Replace("\r", "").Trim();
        }

       
    }

    public class ZCBRecord
    {
        public int PageIndex { get; set; }

        public double YearRate { get; set; }

        public double DayLeft { get; set; }

        public double MinMount { get; set; }

        public int DealCount { get; set; }

        public string ProductionID { get; set; }

        public double BuyIndex 
        {
            get { return YearRate / (DayLeft - 184.0) * 1000 + MinMount / 100.0; }
        }

        public ZCBRecord(string yearRateRaw, string dayLeftRaw, string minMountRaw,int pageIndex,string dealCount,string productionID)
        {

            this.ProductionID = productionID;

            this.YearRate = double.Parse(yearRateRaw.Replace("%",""),CultureInfo.InvariantCulture);

            this.DayLeft = double.Parse(dayLeftRaw, CultureInfo.InvariantCulture);

            this.MinMount = double.Parse(minMountRaw, CultureInfo.InvariantCulture);

            this.DealCount = int.Parse(dealCount);

            this.PageIndex = pageIndex;

        }
    }

   [Serializable]
   public class ZcbFileDb
   {   
        public  int MaxPageCount { get; set; }

        public int MinMountLowerLimit { get; set; }

        public int DayLeftUpperLimit { get; set; }

        public int DealCountUpperLimit { get; set; }

        public int DisplayTopCount { get; set; }

        public int BuyIndexLimit { get; set; }
   }

   

}
