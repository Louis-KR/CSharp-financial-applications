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
            // Input variables
            double S = 50;
            double K = 50;
            double r = 0.05;
            double div = 0.0;
            double T = 0.5;
            int nbperiods = 4; // number of steps
            double dt = T / nbperiods;
            double df = Math.Exp(-(r - div) * dt); // discount factor
            //// VU EN CLASSE : sigma = ln(1 + pu) / sqrt(T / N)
            double sigma = Math.Log(1.2) / Math.Sqrt(T / nbperiods);
            //// Option type default parameters
            string PayoffType = "Put_Option";
            string ExerciseType = "European_Type";

            // For recombining trees
            double pu = 0.2;
            double pd = 0.2;
            // For non-recombining trees
            double ppu = 0.25;
            double ppd = 0.2;

            //// Instantiate Black-Scholes class
            Black_Scholes_Pricer BS = new Black_Scholes_Pricer();

            //// Black-Scholes Conditional Probabilities
            double d1 = (Math.Log(S / K) + (r + 0.5 * Math.Pow(sigma, 2)) * T) / (sigma * Math.Sqrt(T));
            double d2 = d1 - sigma * Math.Sqrt(T);
            //// Black-Scholes Price
            double[] BS_price = BS.BS_price(d1, d2, S, K, sigma, T, r, PayoffType);
            Console.WriteLine("=============================================");
            Console.WriteLine("European Put Option with Black-Scholes : {0}", BS_price[0]);
            Console.WriteLine("==> Delta = {0}", BS_price[1]);
            Console.WriteLine("==> Gamma = {0}", BS_price[2]);
            Console.WriteLine("=============================================");

            //// Instantiate Binomial classes (parent and child)
            Tree_Moves Binomial = new Tree_Moves();

            //// Naive Binomial Model
            double[] moves_Naive = Binomial.Up_Down_Tree("Naive", pu, pd, d1, d2, r, div, sigma, nbperiods, dt);
            double[] European_Naive = Tree_Moves.Binomial_Tree(moves_Naive[0], moves_Naive[1], moves_Naive[2], moves_Naive[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, ExerciseType);
            Console.WriteLine("European Put Option with Naive Binomial : {0}", European_Naive[0]);
            Console.WriteLine("==> Delta = {0}", European_Naive[1]);
            Console.WriteLine("==> Gamma = {0}", European_Naive[2]);
            double[] American_Naive = Tree_Moves.Binomial_Tree(moves_Naive[0], moves_Naive[1], moves_Naive[2], moves_Naive[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, "American_Type");
            Console.WriteLine("American Put Option with Naive Binomial : {0}", American_Naive[0]);
            Console.WriteLine("==> Delta = {0}", American_Naive[1]);
            Console.WriteLine("==> Gamma = {0}", American_Naive[2]);
            Console.WriteLine("=============================================");

            //// CRR Binomial Model
            double[] moves_CRR = Binomial.Up_Down_Tree("CRR", pu, pd, d1, d2, r, div, sigma, nbperiods, dt);
            double[] European_CRR = Tree_Moves.Binomial_Tree(moves_CRR[0], moves_CRR[1], moves_CRR[2], moves_CRR[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, ExerciseType);
            Console.WriteLine("European Put Option with CRR Binomial : {0}", European_CRR[0]);
            Console.WriteLine("==> Delta = {0}", European_CRR[1]);
            Console.WriteLine("==> Gamma = {0}", European_CRR[2]);
            double[] American_CRR = Tree_Moves.Binomial_Tree(moves_CRR[0], moves_CRR[1], moves_CRR[2], moves_CRR[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, "American_Type");
            Console.WriteLine("American Put Option with CRR Binomial : {0}", American_CRR[0]);
            Console.WriteLine("==> Delta = {0}", American_CRR[1]);
            Console.WriteLine("==> Gamma = {0}", American_CRR[2]);
            Console.WriteLine("=============================================");

            //// LR Binomial Model
            double[] moves_LR = Binomial.Up_Down_Tree("LR", ppu, ppd, d1, d2, r, div, sigma, nbperiods, dt);
            double[] European_LR = Tree_Moves.Binomial_Tree(moves_LR[0], moves_LR[1], moves_LR[2], moves_LR[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, ExerciseType);
            Console.WriteLine("European Put Option with LR Binomial : {0}", European_LR[0]);
            Console.WriteLine("==> Delta = {0}", European_LR[1]);
            Console.WriteLine("==> Gamma = {0}", European_LR[2]);
            double[] American_LR = Tree_Moves.Binomial_Tree(moves_LR[0], moves_LR[1], moves_LR[2], moves_LR[3], S, K, r, sigma, div, T, dt, df, nbperiods, PayoffType, "American_Type");
            Console.WriteLine("American Put Option with LR Binomial : {0}", American_LR[0]);
            Console.WriteLine("==> Delta = {0}", American_LR[1]);
            Console.WriteLine("==> Gamma = {0}", American_LR[2]);
            Console.WriteLine("=============================================");

            //// Convergence with Black-Scholes
            Console.WriteLine("Convergence European Put Option : BS - Naive : {0}", BS_price[0] - European_Naive[0]);
            Console.WriteLine("Convergence European Put Option : BS - CRR : {0}", BS_price[0] - European_CRR[0]);
            Console.WriteLine("Convergence European Put Option : BS - LR : {0}", BS_price[0] - European_LR[0]);
            Console.WriteLine("=============================================");
        }

        class Black_Scholes_Pricer  // base class (parent) 
        {
            //// Probability density function of standard normal random variable x
            static double N(double x)
            {
                double n_x = (1 / Math.Sqrt(2 * Math.PI)) * Math.Exp(-0.5 * Math.Pow(x, 2));
                double a1 = 0.4361836;
                double a2 = -0.1201676;
                double a3 = 0.9372980;
                double k_x = 1 / (1 + 0.33267 * x);
                if (x > 0)
                    return (1 - n_x * (a1 * k_x + a2 * Math.Pow(k_x, 2) + a3 * Math.Pow(k_x, 3)));
                else
                    return 1 - N(-x);
            }
            //// Black-Scholes European Options Pricer
            public double[] BS_price(double d1, double d2, double S, double K, double sigma, double T, double r, string PayoffType)
            {
                if (PayoffType == "Call_Option")
                {
                    double[] outputs = new double[3];
                    outputs[1] = N(d1); // delta
                    outputs[2] = N(d1) / (S * sigma * Math.Exp(T)); // gamma
                    outputs[0] = S * N(d1) - K * Math.Exp(-r * T) * N(d2);
                    return outputs;
                }
                else
                {
                    double[] outputs = new double[3];
                    outputs[1] = N(d1) - 1; // delta
                    outputs[2] = N(d1) / (S * sigma * Math.Exp(T)); // gamma
                    outputs[0] = K * N(-d2) * Math.Exp(-r * T) - S * N(-d1);
                    return outputs;
                }
            }
        }

        class Tree_Pricer  // base class (parent) 
        {
            public static double[] Binomial_Tree(double u, double d, double qu, double qd, double S, double K, double r, double sigma, double div, double T, double dt, double df, int nbperiods, string PayoffType, string ExerciseType)
            {
                double option_payoff;
                //// Two dimensional array of length (3, 3) = one more to add the last step of 3 nodes [2,2 2,1 2,0]
                // Option prices array
                double[,] OptionValue = new double[nbperiods + 1, nbperiods + 1];
                // Sotck prices array
                double[,] St = new double[nbperiods + 1, nbperiods + 1];

                //// First loop for "T" values (maturity)
                //// Obtain final ST and Option prices at T (end of the tree) = (2,2) (2,1) (2,0) = (step, node)
                for (int i = nbperiods; i >= 0; i--)
                {
                    //// Compute ST values
                    St[nbperiods, i] = S * (Math.Pow(u, i)) * (Math.Pow(d, nbperiods - i));
                    //// CALL payoffs at T
                    if (PayoffType == "Call_Option")
                        OptionValue[nbperiods, i] = Math.Max(St[nbperiods, i] - K, 0);
                    //// PUT payoffs at T
                    else
                        OptionValue[nbperiods, i] = Math.Max(K - St[nbperiods, i], 0);
                }

                //// Second Loop for "t" values (before maturity)
                //// Contain two loops to access : (step 1, node 0) (step 1, node 1) (step 0, node 0)
                for (int step = nbperiods - 1; step >= 0; step--) // (1) (0)                      
                {
                    for (int node = 0; node < step + 1; node++) // (1,0), (1,1), (0,0)                
                    {
                        //// Stock price at each "t" : [step,node]
                        St[step, node] = S * (Math.Pow(u, node)) * (Math.Pow(d, step - node));

                        //// CALL and PUT payoffs at each "t" : [step,node]
                        if (PayoffType == "Call_Option")
                            option_payoff = Math.Max(St[step, node] - K, 0);
                        else
                            option_payoff = Math.Max(K - St[step, node], 0);

                        //// EUROPEAN and AMERICAN CALL and PUT prices at each [step,node]
                        if (ExerciseType == "European_Type") // EUROPEAN
                            OptionValue[step, node] = df * (qu * OptionValue[step + 1, node + 1] + qd * OptionValue[step + 1, node]);
                        else // AMERICAN
                            OptionValue[step, node] = Math.Max(option_payoff, df * (qu * OptionValue[step + 1, node + 1] + qd * OptionValue[step + 1, node]));
                    }
                    
                }
                // DELTA and GAMMA in t0
                double delta_t0 = (OptionValue[2, 2] - OptionValue[2, 0]) / (St[2, 2] - St[2, 0]);
                double gamma_t0 = (((OptionValue[2, 2] - OptionValue[2, 1]) / (St[2, 2] - St[2, 1]) - (OptionValue[2, 1] - OptionValue[2, 0])) / (St[2, 1] - St[2, 0])) / (((St[2, 2] + St[2, 1]) / 2) - ((St[2, 0] + St[2, 1]) / 2));
               
                double[] outputs = new double[3];
                outputs[0] = OptionValue[0, 0];
                outputs[1] = delta_t0;
                outputs[2] = gamma_t0;

                return outputs;
            }
        }

        class Tree_Moves : Tree_Pricer  // derived class (child)
        {
            public double[] Up_Down_Tree(string Model, double pu, double pd, double d1, double d2, double r, double div, double sigma, int nbperiods, double dt)
            {
                double[] moves = new double[4];
                //// u, d, qu, qd for Naive Binomial Model
                if (Model == "Naive")
                {
                    moves[0] = 1 + pu; //u
                    moves[1] = 1 - pd; //d
                    moves[2] = (Math.Exp((r - div) * dt) - moves[1]) / (moves[0] - moves[1]); //qu
                    moves[3] = 1 - moves[2]; //qd
                }
                //// u, d, qu, qd for CRR Binomial Model
                else if (Model == "CRR")
                {
                    moves[0] = Math.Exp(sigma * Math.Sqrt(dt)); //u
                    moves[1] = 1 / moves[0]; //d
                    moves[2] = (Math.Exp((r - div) * dt) - moves[1]) / (moves[0] - moves[1]); //qu
                    moves[3] = 1 - moves[2]; //qd
                }
                //// u, d, qu, qd for LR Binomial Model
                else
                {
                    double n = nbperiods;
                    double LR_d1 = 0.5 + Math.Sign(d1) * Math.Sqrt(0.25 - 0.25 * Math.Exp(-(Math.Pow((d1 / (n + 1 / 3 + 0.1 / (n + 1))), 2)) * (n + 1 / 6)));
                    double LR_d2 = 0.5 + Math.Sign(d2) * Math.Sqrt(0.25 - 0.25 * Math.Exp(-(Math.Pow((d2 / (n + 1 / 3 + 0.1 / (n + 1))), 2)) * (n + 1 / 6)));
                    moves[0] = Math.Exp((r - div) * dt) * (LR_d1 / LR_d2); //u
                    moves[1] = (Math.Exp((r - div) * dt) - (LR_d2 * moves[0])) / (1 - LR_d2); //d
                    moves[2] = LR_d2; //qu
                    moves[3] = 1 - moves[2]; //qd
                }
                return moves;
            }
        }
    }
}
