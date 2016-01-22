using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using System.Timers;
using nxprice_lib.Robot;
using Microsoft.Practices.Unity;

namespace nxprice_lib
{
    public class NxPriceMgr
    {
        private FileDbEngine<FileDb> dbEngine;
        private FileDb db;
        private Timer timer;
        private RobotFactory factory;
        private bool isBusy = false;
        private UnityContainer container;

        public NxPriceMgr(RobotFactory factory,UnityContainer container)
        {
            this.container = container;
            this.factory = factory;
            this.dbEngine = new FileDbEngine<FileDb> ();                       
        }

        public void Start()
        {
            Initialize();

            TimerElapsedHandler(null,null);

            this.timer.Start();

        }

        private void Initialize()
        {
            this.db = this.dbEngine.LoadFileDB();
            this.container.RegisterInstance<FileDb>(db);
            this.timer = new Timer(this.db.TimerInterval);
            this.timer.Elapsed += new ElapsedEventHandler(TimerElapsedHandler);
        }

        private void TimerElapsedHandler(object sender, ElapsedEventArgs e)
        {
            if (isBusy) return;

            this.isBusy = true;

            foreach (var targetRecord in this.db.TargetRecords)
            {
                if (targetRecord.IsEnabled)
                {
                    IRobot robot = this.factory.CreateRobot(targetRecord.WebSite, this.container);

                    robot.DoJob(targetRecord);
                }
            }


            dbEngine.Save();

            this.isBusy = false;
        }

    }
}
