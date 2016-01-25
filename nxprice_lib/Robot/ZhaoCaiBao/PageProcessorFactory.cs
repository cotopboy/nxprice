using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using nxprice_data;

namespace nxprice_lib.Robot.ZhaoCaiBao
{
    public class PageProcessorFactory
    {
        private UnityContainer container;

        public PageProcessorFactory(UnityContainer container)
        {
            this.container = container;
        }

        public PageProcessor BuildPageProcessor(int pageIndex,TargetRecord jobInfo )
        {
            return this.container.Resolve<PageProcessor>(
                    new ParameterOverride("pageIndex",pageIndex),
                    new ParameterOverride("jobInfo", jobInfo));
        }
    }
}
