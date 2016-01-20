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
    public class AmazonRobot : IRobot
    {
        private TargetRecord jobInfo = null;

        private FileDb db;


        public AmazonRobot(FileDb db)
        {
            this.db = db;
        }

        public void DoJob(TargetRecord jobInfo)
        {
            this.jobInfo = jobInfo;
            this.StartProcess(jobInfo);
        }

        protected void StartProcess(TargetRecord jobInfo)
        {
            try
            {
                string pageHtml = GetPageHtml(jobInfo);
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

        private void SendMessage(TargetRecord jobInfo)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add("2777888@qq.com");

            msg.From = new MailAddress("nxdaigou@gmail.com", "Nx-Daigou Luebeck", System.Text.Encoding.UTF8);

            msg.Subject = jobInfo.Name + "=="+ jobInfo.LastStatus;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = msg.Subject;


            var client = new SmtpClient(this.db.EmailConfig.GmailSmtp, this.db.EmailConfig.GmailPort)
            {
                Credentials = new NetworkCredential(this.db.EmailConfig.GmailAccount, this.db.EmailConfig.GmailPassword),
                EnableSsl = true
            };

            client.Send(msg);
        }

        private string GetCurrentStatus(HtmlNode priceblock_ourprice)
        {
            if (priceblock_ourprice == null) return "NA";
            else return priceblock_ourprice.InnerHtml;
        }

        private static HtmlNode GetPriceBlock(string pageHtml)
        {

            var doc = new HtmlDocument();
            doc.LoadHtml(pageHtml);

            HtmlNode priceblock_ourprice = doc.GetElementbyId("priceblock_ourprice");
            return priceblock_ourprice;
        }

        private static string GetPageHtml(TargetRecord jobInfo)
        {
            WebClient MyWebClient = new WebClient();

            MyWebClient.Credentials = CredentialCache.DefaultCredentials;

            Byte[] pageData = MyWebClient.DownloadData(jobInfo.Url);

            string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            
            //string pageHtml = Encoding.UTF8.GetString(pageData);   //如果获取网站页面采用的是UTF-8，则使用这句
            return pageHtml;
        }

     
    }
}
