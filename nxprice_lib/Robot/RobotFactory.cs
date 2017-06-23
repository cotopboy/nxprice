using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using Microsoft.Practices.Unity;
using nxprice_lib.Robot.DM;
using nxprice_lib.Robot.RM;
using nxprice_lib.Robot.DBStation.Ps;

namespace nxprice_lib.Robot
{
    public class RobotFactory
    {

        public IRobot CreateRobot(WebSite webSite, UnityContainer container)
        {
            if (webSite == WebSite.ZhaoCaiBao) return container.Resolve<ZhaoCaiBaoRobot>();
            if (webSite == WebSite.DM) return container.Resolve<DmRobot>();
            if (webSite == WebSite.RM) return container.Resolve<RmRobot>();
            if (webSite == WebSite.DbStationPs) return container.Resolve<DbStationPsRobot>();
            return null;
        }
    }
}
