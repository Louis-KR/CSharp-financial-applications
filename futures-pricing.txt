using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        { //// string input :div_prices (return dividend yields and future price),
          //// div_yields (return dividend prices and future price),
          //// or other (return an error about input)
            string input_type = "div_yields"; 
            double PV_future = Future(input_type);
            Console.ReadLine();
        }

        static double Future(string input_type)
        { //// dividend prices and yields are fixed, as r, S and period numbers
            double[] div_prices = new double[] { 1.40, 1.45, 1.20, 1.50, 1.50 };
            double[] div_yields = new double[] { 0.014, 0.0145, 0.012, 0.015, 0.015 };
            double[] r = new double[] { 0.05, 0.045, 0.05, 0.055, 0.05 };
            int S = 100;
            double PV_future = 0.0;
            double PV_dividends = 0.0;
            int nPeriods = 5;

            if (string.Equals(input_type, "div_prices") == true)
            {
                double dividend_yields = 0.0;
                for (int t = 0; t < nPeriods; t++)
                {
                    double factor = 1.0 + r[t];
                    double discount_factor = Math.Pow(factor, t);
                    //// calculate dividend prices and yields for each period
                    PV_dividends += div_prices[t] * discount_factor;
                    dividend_yields = div_prices[t] / S;
                    Console.WriteLine("\n dividend_yield : {0}", dividend_yields);
                }
                //// final future price
                PV_future = (S - PV_dividends) / Math.Pow(1.0 + r[nPeriods - 1], nPeriods - 1);
                Console.WriteLine("\n PV_future {0}", PV_future);
                return PV_future;
            }

            else if (string.Equals(input_type, "div_yields") == true)
            {
                double dividends = 0.0;
                for (int t = 0; t < nPeriods; t++)
                {
                    double factor = 1.0 + r[t];
                    double discount_factor = Math.Pow(factor, t);
                    //// calculate dividend prices and yields for each period
                    PV_dividends += (S * div_yields[t]) * discount_factor;
                    dividends = S * div_yields[t];
                    Console.WriteLine("\n dividend : {0}", dividends);
                }
                //// final future price
                PV_future = (S - PV_dividends) / Math.Pow(1.0 + r[nPeriods - 1], nPeriods - 1);
                Console.WriteLine("\n PV_future {0}", PV_future);
                return PV_future;
            }

            else
            {
                Console.WriteLine("Wrong input_type");
            }
            return 0.0;
        }
    }
}
