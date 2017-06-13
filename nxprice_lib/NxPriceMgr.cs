using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Timers;
using nxprice_lib.Robot;
using Microsoft.Practices.Unity;
using System.Diagnostics;
using Utilities.IO;


namespace nxprice_lib
{
    public class StartParameter
    { 
    
    }

    public class NxPriceMgr
    {
        private FileDbEngine<FileDb> dbEngine;
        private FileDb db;
        private Timer timer;
        private Timer restartTimer;
        private RobotFactory factory;
        private bool isBusy = false;
        private bool isInitialize = false;
        private UnityContainer container;

        public NxPriceMgr(RobotFactory factory,UnityContainer container)
        {
            this.container = container;
            this.container.RegisterInstance(container);
            this.factory = factory;
            this.dbEngine = new FileDbEngine<FileDb> ();                       
        }

        public void StartOnce()
        {
            Initialize();
            TimerElapsedHandler(null, null);
        }

        public void Start()
        {
            Initialize();

            TimerElapsedHandler(null,null);

            this.timer.Start();

        }

        private void Initialize()
        {
            if (this.isInitialize) return;
            
            this.db = this.dbEngine.LoadFileDB();
            this.container.RegisterInstance<FileDb>(db);
            this.timer = new Timer(this.db.TimerInterval);
            this.timer.Elapsed += new ElapsedEventHandler(TimerElapsedHandler);
            this.isInitialize = true;

            this.restartTimer = new Timer(1000 * 3600 * 24);
            restartTimer.Elapsed += new ElapsedEventHandler(restartTimer_Elapsed);
            restartTimer.Start();

            if(db.WebProxy.IsEnablded)
                Console.WriteLine("Web Proxy :" + db.WebProxy.ProxyServer);


        }

        private void restartTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            Process.Start(DirectoryHelper.CurrentProcessFullName);

            System.Threading.Thread.Sleep(10000);

            Environment.Exit(0);
        }

        private void TimerElapsedHandler(object sender, ElapsedEventArgs e)
        {
            if (isBusy) return;

            this.isBusy = true;

            foreach (var targetRecord in this.db.TargetRecords)
            {
                if (targetRecord.IsEnabled)
                {
                    try
                    {
                        IRobot robot = this.factory.CreateRobot(targetRecord.WebSite, this.container);
                        robot.DoJob(targetRecord);
                    }
                    catch 
                    {
                        Console.WriteLine("Error");
                    }
                        
                }
            }

            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("=====================================");
            Console.WriteLine("=====================================");
            Console.WriteLine();
            Console.ResetColor();


            dbEngine.Save(false);

            this.isBusy = false;
        }

    }
}
