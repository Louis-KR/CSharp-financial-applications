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
            //// Initiate two matrix A & B of integers
            int[,] A = { { 2, 4, 6 }, { 4, 6, 8 }, { 6, 8, 10 } };
            int[,] B = { { 1, 1, 1 }, { 2, 2, 2 }, { 3, 3, 3 } };

            /// Addition of two matrix A & B
            int[,] A_plus_B = Addition(A, B);
            Console.WriteLine("\n A & B matrix addition :");
            Display_Matrix(A_plus_B);

            /// Substraction of two matrix A & B
            int[,] A_minus_B = Substraction(A, B);
            Console.WriteLine("\n A & B matrix substraction :");
            Display_Matrix(A_minus_B);

            /// Multiplication of two matrix A & B
            int[,] A_times_B = Multiplication(A, B);
            Console.WriteLine("\n A & B matrix multiplication :");
            Display_Matrix(A_times_B);
        }

        //// MATRIX ADDITION
        static int[,] Addition(int[,] A, int[,] B)
        {
            //// Get length of each matrix rows and columns
            int rowsA = A.GetLength(0);
            int columnsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int columnsB = B.GetLength(1);
            //// Initate an empty matrix with A & B dimensions
            int[,] matrix_addition = new int[rowsA, columnsB];

            //// Test if dimensions are similar
            if (columnsA != rowsB)
            {
                Console.WriteLine("Matrix can not be added because of different dimensions");
                throw new Exception("Error");
            }
            else
            {
                //// Iterate over each i row and j column
                //// Add Aij & Bij
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < columnsB; j++)
                    {
                        matrix_addition[i, j] = A[i, j] + B[i, j];
                    }
                }
            }
            return matrix_addition;
        }

        //// MATRIX SUBSTRACTION
        static int[,] Substraction(int[,] A, int[,] B)
        {
            //// Get length of each matrix rows and columns
            int rowsA = A.GetLength(0);
            int columnsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int columnsB = B.GetLength(1);
            //// Initate an empty matrix with A & B dimensions
            int[,] matrix_substraction = new int[rowsA, columnsB];

            //// Test if dimensions are similar
            if (columnsA != rowsB)
            {
                Console.WriteLine("Matrix can not be substracted because of different dimensions");
                throw new Exception("Error");
            }

            else
            {
                //// Iterate over each i row and j column
                //// Substract Aij & Bij
                for (int i = 0; i < rowsA; i++)
                {
                    for (int j = 0; j < columnsB; j++)
                    {
                        matrix_substraction[i, j] = A[i, j] - B[i, j];
                    }
                }
            }
            return matrix_substraction;
        }

        //// MATRIX MULTIPLICATION
        static int[,] Multiplication(int[,] A, int[,] B)
        {
            //// Get length of each matrix rows and columns
            int rowsA = A.GetLength(0);
            int columnsA = A.GetLength(1);
            int rowsB = B.GetLength(0);
            int columnsB = B.GetLength(1);
            //// Initate the variable to store the multiplications
            int cumulative_multiplications;
            //// Initate an empty matrix with A & B dimensions
            int[,] matrix_multiplication = new int[rowsA, columnsB];

            //// Test if dimensions are similar
            if (columnsA != rowsB)
            {
                Console.WriteLine("Matrix can not be substracted because of different dimensions");
                throw new Exception("Error");
            }
            else
            {
                //// Iterate over each i row and j column
                //// Multiply Aij & Bij
                for (int i=0; i<rowsA; i++)
                {
                    for (int j=0; j<columnsB; j++)
                    {
                        //// Total of cumulative multiplications
                        cumulative_multiplications = 0;
                        for (int k=0; k<columnsA; k++)
                        {
                            cumulative_multiplications += A[i,k] * B[k,j];
                        }
                        matrix_multiplication[i,j] = cumulative_multiplications;
                    }
                }
                return matrix_multiplication;
            }
        }

        //// DISPLAY MATRIX
        static void Display_Matrix(int[,] A)
        {
            //// PRINT each i row and j column
            for (int i=0; i<A.GetLength(0); i++)
            {
                for (int j=0; j<A.GetLength(1); j++)
                    Console.Write($"{A[i, j]} ");
                Console.WriteLine();
            }
        }

    }
}
