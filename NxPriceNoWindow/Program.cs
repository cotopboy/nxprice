using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using nxprice_lib;

namespace NxPriceNoWindow
{
    class Program
    {
        static void Main(string[] args)
        {
            NxPriceMain main = new NxPriceMain();
            main.Start();
            Console.ReadLine();
        }
    }
}
