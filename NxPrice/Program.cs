using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_data;
using Microsoft.Practices.Unity;
using nxprice_lib;

namespace NxPrice
{
    class Program
    {
        static void Main(string[] args)
        {
            NxPriceMain main = new NxPriceMain();
            main.Start();

            /*
            FileDb db = new FileDb();

            db.TargetRecords = new List<TargetRecord>()
            {
                new TargetRecord(){ Name = "LightAir 50 Surface", PullingIntervalSec = 60*30, Url = "http://www.amazon.de/Brita-Wasserfilter-Jahrespackung-mitternachtsblau-Edition/dp/B001EHF29W/ref=pd_sim_60_5?ie=UTF8&dpID=419P8YuAvsL&dpSrc=sims&preST=_AC_UL160_SR160%2C160_&refRID=1EH58MCDHQH5EDPFKEAX", WebSite=WebSite.Amazon}
            };

            var engine = new FileDbEngine<FileDb>();

            engine.SetDB(db);

            engine.Save();*/
        }
    }
}
