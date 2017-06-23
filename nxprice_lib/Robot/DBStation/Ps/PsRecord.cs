using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace nxprice_lib.Robot.DBStation.Ps
{
    public class PsRecord
    {
        public string OrderId { get; set; }

        public string DHLTracingNo { get; set; }

        public string RecipientName { get; set; }

        public string CnAddress { get; set; }

        public string EMSTracingNo { get; set; }

        public string Remark { get; set; }

        public string Email { get; set; }

        public string AgentName { get; set; }
    }
}
