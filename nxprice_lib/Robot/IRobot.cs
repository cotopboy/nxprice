using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;

namespace nxprice_lib.Robot
{
    public interface IRobot
    {
        void DoJob(TargetRecord jobInfo);
    }
}
