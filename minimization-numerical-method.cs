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
            double a = 2; //// starting a
            double b = a + 0.1; //// starting b
            double tolerance_limit = 1e-5;
            double error_to_minimize = 1;
            double maximum_of_iterations = 1000;
            double result = Find_Root(a, b, tolerance_limit, error_to_minimize, maximum_of_iterations);
        }

        static double function(double x)
        {
            return x * x + 2;
        }

        static double Find_Root(double a, double b, double tolerance_limit, double error_to_minimize, double maximum_of_iterations)
        {
            int counter = 0;
            while (error_to_minimize > tolerance_limit)
            {
                //// Ad an iteration to the counter to stop if "maximum_of_iterations" is reached
                counter++;
                if (counter >= maximum_of_iterations)
                {
                    Console.WriteLine("Maximum of iterations is reached");
                    return counter;
                }
                //// Compute c
                double c = b - function(b) * ((b - a) / (function(b) - function(a)));
                //// Compute distance (to minimize) between b and c
                error_to_minimize = Math.Abs(b - c);
                //// Assign a and b for the next iteration
                a = b;
                b = c;
                Console.WriteLine("c = {0}", b);
                Console.WriteLine("Error_to_minimize {0}", error_to_minimize);
            }
            return error_to_minimize;
        }
    }
}
