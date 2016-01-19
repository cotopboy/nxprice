using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nxprice_data
{
    [Serializable]
    public class TargetRecord
    {
        public string Name { get; set; }

        public string Url { get; set; }

        public WebSite WebSite { get; set; }

        public int PullingIntervalSec { get; set; }

    }
}
