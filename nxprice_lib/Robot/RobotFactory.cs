using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using Microsoft.Practices.Unity;

namespace nxprice_lib.Robot
{
    public class RobotFactory
    {

        public IRobot CreateRobot(WebSite webSite, UnityContainer container)
        {
            if (webSite == WebSite.Amazon) return container.Resolve<AmazonRobot>();

            if (webSite == WebSite.ZhaoCaiBao) return container.Resolve<ZhaoCaiBaoRobot>();

            return null;
        }
    }
}
