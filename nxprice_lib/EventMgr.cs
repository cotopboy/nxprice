using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data.Event;
using nxprice_data;

namespace nxprice_lib
{
    public class EventMgr
    {
        public event OnNewResultHandler OnNewResult;

        public void RaiseOnNewResultEvent(List<ZCBRecord> retList)
        {
            if (OnNewResult != null)
            {
                OnNewResult(retList);
            }
        }
    }
}
