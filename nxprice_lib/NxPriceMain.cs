using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using nxprice_lib.Robot.ZhaoCaiBao;

namespace nxprice_lib
{
    public class NxPriceMain
    {
        public void Start()
        {
            UnityContainer nx = new UnityContainer();

            nx.RegisterInstance(nx);

            nx.RegisterType<EventMgr>(new ContainerControlledLifetimeManager());
            nx.RegisterType<MaxiPageMgr>(new ContainerControlledLifetimeManager());

            var mgr = nx.Resolve<NxPriceMgr>();
             
            mgr.Start();
        }

    }
}
