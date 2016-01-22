using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Net;
using HtmlAgilityPack;
using System.Net.Http;
using System.Threading;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.FileFormats.Zip;
using Utilities.IO;
using Utilities.IO.ExtensionMethods;
using Utilities.Math.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Serialization;
using Utilities.Environment.Email;
using System.Net.Mail;


namespace nxprice_lib.Robot
{
    public class AmazonRobot : Robot,IRobot
    {

        public AmazonRobot(FileDb db) : base(db)
        {
           
        }

        public override void DoJob(TargetRecord jobInfo)
        {
            this.jobInfo = jobInfo;
            this.StartProcess(jobInfo);
        }

        protected void StartProcess(TargetRecord jobInfo)
        {
            try
            {
                string pageHtml = GetPageHtml(jobInfo.Url, false,Encoding.Default);
                HtmlNode priceblock_ourprice = GetPriceBlock(pageHtml);

                string currentStatus = GetCurrentStatus(priceblock_ourprice);
                if (jobInfo.LastStatus.IsNullOrEmpty())
                {
                    jobInfo.LastStatus = currentStatus;
                }
                else
                {
                    if (jobInfo.LastStatus != currentStatus)
                    {
                        jobInfo.LastStatus = currentStatus;
                        jobInfo.LastChangedTime = DateTime.Now;

                        if (jobInfo.HistoryItems == null) jobInfo.HistoryItems = new List<HistoryItem>();

                        jobInfo.HistoryItems.Add(new HistoryItem() { Status = jobInfo.LastStatus, Time = DateTime.Now });

                        SendMessage(jobInfo);
                    }
                    else
                    { 
                        // the same as before . 
                        // do nothing.
                    }
                }

            }
            catch (WebException webEx)
            {
                Console.WriteLine(webEx.Message.ToString());
            }

        }

        private string GetCurrentStatus(HtmlNode priceblock_ourprice)
        {
            if (priceblock_ourprice == null) return "NA";
            else return priceblock_ourprice.InnerHtml;
        }

        private  HtmlNode GetPriceBlock(string pageHtml)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);

            HtmlNode priceblock_ourprice = doc.GetElementbyId("priceblock_ourprice");
            return priceblock_ourprice;
        }

     

     
    }
}
