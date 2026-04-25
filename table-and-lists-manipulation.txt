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
            //// Create table of integers
            int[] numbers = CreateTable();
            //// Display integers
            DisplayTable(numbers);
            //// Compute and display mean of integers
            double mean = MeanComputation(numbers);
            //// Compute and display standard deviation of integers
            double std = StandardDeviationComputation(mean, numbers);
            //// Display even numbers in first and odd numbers in second
            ReorganizeTable(numbers);
        }

        static double MeanComputation(int[] numbers) //// MEAN COMPUTATION
        {
            int length = numbers.Length;
            double sum = 0; //// initialize
            //// iterate over each number into the list of numbers
            foreach (int number in numbers)
            {
                sum = sum + number; //// add each number
            }
            double mean = sum / length; //// divide by N
            Console.WriteLine("\n Mean : {0}", mean);
            return mean;
        }

        static double StandardDeviationComputation(double mean, int[] numbers) //// STD COMPUTATION
        {
            int length = numbers.Length;
            double sum = 0;
            //// iterate over each number into the list of numbers
            foreach (int number in numbers)
            {
                sum = sum + Math.Pow((number - mean), 2) ; //// take the square difference
            }
            double std = Math.Sqrt(sum / length); //// sqrt of the variance to obtain the std
            Console.WriteLine("\n Standard Deviation : {0}", std);
            return std;
        }

        static int[] CreateTable() //// CREATE TABLE
        {
            //// Specified numbers (string format) seperated by ';'
            Console.WriteLine("Specify the numbers separeted by ';'"); string string_inputs = Console.ReadLine();
            String[] strings = string_inputs.Split(";");
            //// Transform strings into integers
            int[] numbers = Array.ConvertAll(strings, s => int.Parse(s));
            return numbers;
        }

        static void DisplayTable(int[] numbers) //// PRINT TABLE
        {
            foreach (int number in numbers)
            {
                Console.WriteLine(number); //// print
            }
        }

        static void ReorganizeTable(int[] numbers)
        {
            //// create two spaces to store odd and even numbers
            List<int> evens = new List<int>();
            List<int> odds = new List<int>();
            int length = numbers.Length;
            foreach (int number in numbers)
            {
                double result_to_check = number % 2;
                if (result_to_check == 0)
                { evens.Add(number); } //// Evens in first
                else
                { odds.Add(number); } //// Odds in second
            }
            DisplayTable(evens.ToArray());
            DisplayTable(odds.ToArray());
        }
       
    }
}
