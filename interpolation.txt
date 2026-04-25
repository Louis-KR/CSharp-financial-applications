using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

namespace ConsoleApp1
{
    class Program
    {
        static void Main()
        {
            //// Initiate parameters
            double x = 0.5;
            double x0 = 3;
            double x1 = 0;

            double y0 = ;
            double y1 = ;

            //// BS Price
            double BS_price = Interpolate(x, x0, x1);
            Console.WriteLine("=============================================");
            Console.WriteLine("Result of interpolation g(x) : {0}", BS_price);
            Console.WriteLine("=============================================");
        }

        //// Interpolate :
        static double Interpolate(double x, double x0, double x1)
        {
            double alpha = (x - x0) / (x1 - x0);
            double g = alpha * x1 + (1 - alpha) * x0;
            return g;
        }
    }
} 
