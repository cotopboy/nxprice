using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Globalization;

namespace nxprice_data
{
    [Serializable]
    public class ZCBRecord
    {
        public int PageIndex { get; set; }

        public double YearRate { get; set; }

        public double DayLeft { get; set; }

        public double MinMount { get; set; }

        public int DealCount { get; set; }

        public string ProductionID { get; set; }

        public double BuyIndex  { get; set; }

        public int ItemIndex { get; set; }

        public ZCBRecord()
        {

        }

        public ZCBRecord(string yearRateRaw, string dayLeftRaw, string minMountRaw, int pageIndex, string dealCount, string productionID,int itemIndex)
        {
            this.ProductionID = productionID;

            this.YearRate = double.Parse(yearRateRaw.Replace("%", ""), CultureInfo.InvariantCulture);

            this.DayLeft = double.Parse(dayLeftRaw, CultureInfo.InvariantCulture);

            this.MinMount = double.Parse(minMountRaw, CultureInfo.InvariantCulture);

            this.DealCount = int.Parse(dealCount);

            this.PageIndex = pageIndex;

            this.BuyIndex = (double)(this.GetBuyIndex((decimal)YearRate, (decimal)DayLeft));

            this.ItemIndex = itemIndex;
        }

        private decimal GetBuyIndex(decimal YearRate, decimal DayLeft,decimal momey = 10000)
        {
            decimal InputProfil = YearRate / 100m * (DayLeft) / 365m * momey;
            decimal OutputProfil = 3.5m / 100m * 185 / 365m * momey;

            decimal platformFee = momey * 0.2m / 100m;


            decimal ret = InputProfil - OutputProfil - platformFee;

            return ret / (DayLeft - 184m) / 10000m * 365m * 100;
            
        }
    }
}
