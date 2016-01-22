using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nxprice_data
{
    [Serializable]
    public class TargetRecord
    {
        public bool IsEnabled { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }

        public WebSite WebSite { get; set; }

        public int PullingIntervalSec { get; set; }

        public DateTime LastChangedTime { get; set; }
            
        public string LastStatus { get; set; }

        public List<HistoryItem> HistoryItems { get; set; }

    }

    [Serializable]
    public class HistoryItem
    {
        public DateTime Time { get;set; }

        public string Status  { get;set;}
    }
}
