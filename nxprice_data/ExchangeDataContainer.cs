using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nxprice_data
{
    [Serializable]
    public class ExchangeDataContainer
    {
        public List<ZCBRecord> FisrtList { get; set; }

        public List<ZCBRecord> SecondList { get;set; }
    }
}
