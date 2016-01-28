using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nxprice_lib.Robot.ZhaoCaiBao
{
    [Serializable]
    public class ZcbFileDb
    {
        public int MaxThreadCount { get; set; }

        public int MaxPageCount { get; set; }

        public int MinMountLowerLimit { get; set; }

        public int DayLeftUpperLimit { get; set; }

        public int DealCountUpperLimit { get; set; }

        public int DisplayTopCount { get; set; }

        public int BuyIndexLimit { get; set; }

        public bool BuyIndexSendEmailEnabled { get; set; }

        public int BuyIndexSendEmailLimit { get; set; }
    }
}
