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
using nxprice_lib.Robot.ZhaoCaiBao;


namespace nxprice_lib.Robot
{
    public class ZhaoCaiBaoRobot :Robot,IRobot
    {
        private static volatile bool isBusy = false;

        private static List<ZCBRecord> RecordList = new List<ZCBRecord>();

        private List<ManualResetEvent> EventList = new List<ManualResetEvent>();
        private ZcbFileDb zcbFileDb = null;
        private PageProcessorFactory pageProcessorFactory;
        private Speaker speaker;

        public ZhaoCaiBaoRobot(FileDb db, PageProcessorFactory pageProcessorFactory,Speaker speaker) : base(db)
        {
            this.pageProcessorFactory = pageProcessorFactory;
            this.speaker = speaker;
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
                if (EventList.Count == this.zcbFileDb.MaxThreadCount )
                {
                    int index = WaitHandle.WaitAny(EventList.ToArray());
                    EventList.RemoveAt(index);
                }

                PageProcessor pageProcessor = this.pageProcessorFactory.BuildPageProcessor(i, this.jobInfo);
                pageProcessor.SetReturnListRef(RecordList);
               
                EventList.Add(pageProcessor.EventHandle);
                
                new Action(pageProcessor.GetPageRecord).BeginInvoke(null, null);
            }
            WaitHandle.WaitAll(EventList.ToArray());


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
                this.speaker.Say(first.BuyIndex.Round(0).ToString());
            }

            ExportListToBuyUi(HittedRecord);

            
        }

        private void ExportListToBuyUi(List<ZCBRecord> list)
        {
            string dirExchange = DirectoryHelper.CombineWithCurrentExeDir("ZcbListExchange");
            if (!Directory.Exists(dirExchange)) Directory.CreateDirectory(dirExchange);

            string filePath = Path.Combine(dirExchange, "list.xml");

            FileInfo file = new FileInfo(filePath);
            if (file.Exists) file.Delete();

            
            var dbEngine = new FileDbEngine<List<ZCBRecord>>(filePath);
            dbEngine.SetDB(list);

            dbEngine.Save();

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


       
    }


}
