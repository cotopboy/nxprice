using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Net;
using System.IO;
using Utilities.DataTypes.ExtensionMethods;
using Utilities.IO.ExtensionMethods;
using Utilities.Reflection.ExtensionMethods;
using Utilities.Math.ExtensionMethods;


namespace nxprice_lib.Robot.DBStation.Ps
{
    public class DbStationPsRobot : Robot, IRobot
    {
        private static readonly string loginUrl = "http://db-station.com/index.php?s=member&c=login";
        private static readonly string hadDanghaoUrl = "http://db-station.com/index.php?s=shop&c=member&status=4";

        private static readonly string WaitingDanghaoUrl = "http://db-station.com/index.php?s=shop&c=member&status=3";

        private static readonly string loginData = "data%5Bback%5D=http%253A%252F%252Fdb-station.com%252F&data%5Busername%5D=cotopboy&data%5Bpassword%5D=Nx470%40%40%40&submit=%E7%99%BB+%E5%BD%95";

        private string hadDanghaoOrderIDHistoryFile = "ps_had_Danghao.txt";
        private string waitingDanghaoOrderIDHistoryFile = "ps_waiting_Danghao.txt";

        private static DateTime lastProcessTime = DateTime.MinValue;

        private readonly PsInfoExtractor extractor;
        public DbStationPsRobot(FileDb db ,PsInfoExtractor extractor)
            : base(db)
        {
            this.extractor = extractor;
        }

        public override void DoJob(TargetRecord jobInfo)
        {
            if (lastProcessTime.AddSeconds(jobInfo.PullingIntervalSec) >= DateTime.Now) return;

            Console.WriteLine("DbStationPsRobot...");

            var client = new CookieAwareWebClient();
            client.Login(loginUrl, loginData);            
            var hadDanghaoContent = client.DownloadData(hadDanghaoUrl);
            var WaitingDanghaoContent = client.DownloadData(WaitingDanghaoUrl);

            var hadDanghaoList = extractor.GetPsRecords(hadDanghaoContent);
            var waitingDanghaoList = extractor.GetPsRecords(WaitingDanghaoContent);

            ProcessHadDanghao(hadDanghaoList);

            ProcessWaitingDanghao(waitingDanghaoList);

            lastProcessTime = DateTime.Now;

            Console.WriteLine("DbStationPsRobot Finished");
        }

        private void ProcessWaitingDanghao(List<PsRecord> waitingDanghaoList)
        {
            List<string> processedId = new List<string>();
            var waitingDanghaoIdHistory = File.ReadAllLines(waitingDanghaoOrderIDHistoryFile);
            foreach (var item in waitingDanghaoList)
            {
                if (waitingDanghaoIdHistory.Contains(item.OrderId)) continue;

                SendWaitingDanghaoEmail(item);

                processedId.Add(item.OrderId);
            }

            File.AppendAllLines(waitingDanghaoOrderIDHistoryFile, processedId);
        }

     
        private void ProcessHadDanghao(List<PsRecord> hadDanghaoList)
        {
            List<string> processedId = new List<string>();
            var hadDanghaoIdHistory = File.ReadAllLines(hadDanghaoOrderIDHistoryFile);

            foreach (var item in hadDanghaoList)
            {
                if (hadDanghaoIdHistory.Contains(item.OrderId)) continue;

                SendHadDanghaoEmail(item);

                processedId.Add(item.OrderId);
            }
            File.AppendAllLines(hadDanghaoOrderIDHistoryFile, processedId);
      

        }

        private void SendHadDanghaoEmail(PsRecord item)
        {
            if (item.Email.IsNullOrEmpty()) return;

            var header = "[" + item.RecipientName + "]" + "国际订单号已生成";
            var body = "国际运单号 EMS转单号: " + item.EMSTracingNo + Environment.NewLine + Environment.NewLine;
            body += "通过百度查询: " + "http://www.baidu.com/s?wd=" + item.EMSTracingNo.Trim() + Environment.NewLine + Environment.NewLine;
            body += "通过中国EMS网站进行更进一步的查询： http://www.ems.com.cn " + Environment.NewLine + Environment.NewLine;
            body += "通过EMS全国统一服务电话人工查询及问题反馈: 11183 " + Environment.NewLine + Environment.NewLine;
            body += "谢谢 :)";


            SendMessage(item.Email, "NxDaigou 张锐锋", header, body);
        }

        private void SendWaitingDanghaoEmail(PsRecord item)
        {
            if (item.Email.IsNullOrEmpty()) return;

            var header = "[" + item.RecipientName + "]" + " 包裹德国境内单号已生成";
            var body = "";
            body += "运单编号: " + item.OrderId + Environment.NewLine + Environment.NewLine;
            body += "收件人地址: " + item.CnAddress + Environment.NewLine + Environment.NewLine;

            body += "德国境内物流查询: " + "http://nolp.dhl.de/nextt-online-public/set_identcodes.do?lang=en&idc=" + item.DHLTracingNo + Environment.NewLine + Environment.NewLine;
            body += "等包裹到物流仓库,并且报关之后,才会生成国际运单号" + Environment.NewLine + Environment.NewLine;
            body += "到时候我再发邮件通知你 \n" + Environment.NewLine;
            body += "谢谢 :)";

            SendMessage(item.Email, "NxDaigou 张锐锋", header, body);
        }

    
    }
}
