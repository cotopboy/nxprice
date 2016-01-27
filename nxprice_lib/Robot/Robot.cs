using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Net;
using System.Net.Mail;

namespace nxprice_lib.Robot
{
    public abstract class Robot : IRobot
    {
        public abstract void DoJob(TargetRecord jobInfo);
        
        protected TargetRecord jobInfo = null;
        protected FileDb db;

        public Robot(FileDb db)
        {
            this.db = db;
        }

        protected string GetPageHtml(string url,bool isUseProxy, Encoding encoding = null)
        {
            WebClient MyWebClient = new WebClient();

            MyWebClient.Credentials = CredentialCache.DefaultCredentials;
            
            var webProxyConfig = db.WebProxy;
            if (db.WebProxy.IsEnablded && isUseProxy)
            {
                WebProxy myProxy = new WebProxy();
                Uri newUri = new Uri("http://node-fr.vnet.link:210");
                myProxy.Address = newUri;
                myProxy.Credentials = new NetworkCredential(webProxyConfig.UserName, webProxyConfig.Password);
                MyWebClient.Proxy = myProxy;

            }

            Byte[] pageData = MyWebClient.DownloadData(url);


            if (encoding == null) encoding = Encoding.UTF8;
            //string pageHtml = Encoding.Default.GetString(pageData);  //如果获取网站页面采用的是GB2312，则使用这句            
            string pageHtml = encoding.GetString(pageData);   //如果获取网站页面采用的是UTF-8，则使用这句
            return pageHtml;
        }

        public string GB2312ToUtf8(string gb2312String)
        {
            Encoding fromEncoding = Encoding.GetEncoding("gb2312");
            Encoding toEncoding = Encoding.UTF8;
            return EncodingConvert(gb2312String, fromEncoding, toEncoding);
        }

        public string EncodingConvert(string fromString, Encoding fromEncoding, Encoding toEncoding)
        {
            byte[] fromBytes = fromEncoding.GetBytes(fromString);
            byte[] toBytes = Encoding.Convert(fromEncoding, toEncoding, fromBytes);

            string toString = toEncoding.GetString(toBytes);
            return toString;
        }

        protected void SendMessage(string Msg)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add("nxdaigou@gmail.com");

            msg.From = new MailAddress("cotopboy@gmail.com", "招财宝", System.Text.Encoding.UTF8);

            msg.Subject = Msg;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body ="";


            var client = new SmtpClient(this.db.EmailConfig.GmailSmtp, this.db.EmailConfig.GmailPort)
            {
                Credentials = new NetworkCredential(this.db.EmailConfig.GmailAccount, this.db.EmailConfig.GmailPassword),
                EnableSsl = true
            };

            client.Send(msg);
        }

        protected void SendMessage(TargetRecord jobInfo)
        {
            System.Net.Mail.MailMessage msg = new System.Net.Mail.MailMessage();

            msg.To.Add("2777888@qq.com");

            msg.From = new MailAddress("nxdaigou@gmail.com", "Nx-Daigou Luebeck", System.Text.Encoding.UTF8);

            msg.Subject = jobInfo.Name + "==" + jobInfo.LastStatus;
            msg.SubjectEncoding = System.Text.Encoding.UTF8;
            msg.Body = msg.Subject;


            var client = new SmtpClient(this.db.EmailConfig.GmailSmtp, this.db.EmailConfig.GmailPort)
            {
                Credentials = new NetworkCredential(this.db.EmailConfig.GmailAccount, this.db.EmailConfig.GmailPassword),
                EnableSsl = true
            };

            client.Send(msg);
        }
    }
}
