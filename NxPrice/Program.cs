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
        }
    }
}
