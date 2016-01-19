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
                new TargetRecord(){ Name = "LightAir 50 Surface", PullingIntervalSec = 60*30, Url = "http://www.amazon.de/Lightair-1015674-LightAir-IonFlow-Surface/dp/B000Z24WGQ/ref=sr_1_2?ie=UTF8&qid=1453222178&sr=8-2&keywords=lightair", WebSite=WebSite.Amazon}
            };

            var engine = new FileDbEngine<FileDb>();

            engine.SetDB(db);

            engine.Save();*/
        }
    }
}
