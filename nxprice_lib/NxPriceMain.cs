using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;

namespace nxprice_lib
{
    public class NxPriceMain
    {
        public void Start()
        {
            UnityContainer nx = new UnityContainer();

            var mgr = nx.Resolve<NxPriceMgr>();

            mgr.Start();

        }

    }
}
