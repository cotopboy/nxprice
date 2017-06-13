using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;

namespace nxprice_lib.Robot.RM
{
    public class RmRobot : Robot, IRobot
    {

        public RmRobot(FileDb db)
            : base(db)
        {

        }

        public override void DoJob(TargetRecord jobInfo)
        {
            this.jobInfo = jobInfo;

            DoJobInternal();
        }

        private void DoJobInternal()
        {
            string content = this.GetPageHtml(jobInfo.Url,false);

            if (content.Contains("Lieferung am"))
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine(DateTime.Now.ToString("s") + " " + jobInfo.Name.PadRight(10) + "==> OK");
                Console.ResetColor();

                if(jobInfo.LastStatus != "A")
                {
                    SendMessage(jobInfo.Name, "[NxRobot] " + jobInfo.Name + " OK","");
                    jobInfo.LastStatus = "A";
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(DateTime.Now.ToString("s") + " " + jobInfo.Name.PadRight(10) + "==> N/A");
                Console.ResetColor();

                if (jobInfo.LastStatus != "N/A")
                {
                    SendMessage(jobInfo.Name, "[NxRobot] " +  jobInfo.Name + " N/A ","");
                    jobInfo.LastStatus = "N/A";
                }
            }
                
        }


    }
}
