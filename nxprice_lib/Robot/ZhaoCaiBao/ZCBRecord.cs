using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace nxprice_lib.Robot.ZhaoCaiBao
{
    public class ZCBRecord
    {
        public int PageIndex { get; set; }

        public double YearRate { get; set; }

        public double DayLeft { get; set; }

        public double MinMount { get; set; }

        public int DealCount { get; set; }

        public string ProductionID { get; set; }

        public double BuyIndex
        {
            get { return YearRate / (DayLeft - 184.0) * 1000 + MinMount / 100.0; }
        }

        public ZCBRecord(string yearRateRaw, string dayLeftRaw, string minMountRaw, int pageIndex, string dealCount, string productionID)
        {

            this.ProductionID = productionID;

            this.YearRate = double.Parse(yearRateRaw.Replace("%", ""), CultureInfo.InvariantCulture);

            this.DayLeft = double.Parse(dayLeftRaw, CultureInfo.InvariantCulture);

            this.MinMount = double.Parse(minMountRaw, CultureInfo.InvariantCulture);

            this.DealCount = int.Parse(dealCount);

            this.PageIndex = pageIndex;

        }
    }
}
