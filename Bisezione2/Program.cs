using System;
using System.Collections.Generic;

namespace Bisezione2
{
    class Program
    {
        static double a;
        static double b;
        static double c;
        static double functionCenterValue = 0;
        static double functionLeftExtremeValue = 0;
        static double functionRightExtremeValue = 0;
        static bool resultFound = false;
        static int unknownFactorsNumber = 0;
        static double constant = 0;
        static string[] factors;
        static string[] elements;
        static List<double> coefficents = new List<double>();
        static List<double> exponents = new List<double>();

        static void Main(string[] args)
        {
            Console.Title = "Bisezione";
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("Istruzioni:\n\nI polinomi devono rispettare il teorema degli 0.\nDevono essere inseriti in forma normale e ogni loro termine deve essere separato da uno spazio\nOgni termine con icognita deve contenere un coefficente, un'incognita e un esponente, anche qualora corrispondano a 1." +
                              "\nI termini noti devono essere inseriti come ultimi.\nLa moltiplicazione viene rappresentata con * (asterisco), l'elevazione a potenza con ^.\nI simboli + e - devono essere uniti al termine successivo.\nEsempio: n*x^m +n*x^m -n*x^m +n" +
                              "\nEsempio: 2*x^3 +1*x^2 -5*x^1 -5");

            Console.WriteLine("\nInserisci l'equazione:");
            string userInput;
            userInput = Console.ReadLine();
            factors = userInput.Split(' ');
            foreach (string factor in factors)
            {
                unknownFactorsNumber++;
                //DEBUG
                //Console.WriteLine(factor);
                if (factor.Contains('x'))
                {
                    if (factor.Contains('*') && factor.Contains('^'))
                    {
                        elements = factor.Split('*', '^');
                        coefficents.Add(Convert.ToDouble(elements[0]));
                        exponents.Add(Convert.ToDouble(elements[2]));
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nInput formattato in modo erroneo. Riprovare.");
                        Console.ForegroundColor = ConsoleColor.White;
                        unknownFactorsNumber = 0;
                        constant = 0;
                        Main(args);
                    }
                    //DEBUG
                    //foreach (string element in elements)
                    //{
                    //    Console.WriteLine(element);
                    //}
                }
                else
                {
                    constant = Convert.ToDouble(factor);
                    unknownFactorsNumber--;
                }
            }

            Console.WriteLine("\nInserisci punto a:");
            a = Convert.ToDouble(Console.ReadLine());
            Console.WriteLine("Inserisci punto b:");
            b = Convert.ToDouble(Console.ReadLine());

            int n = 0;

            while (resultFound == false && n < 20)
            {
                n++;
                ComputeResults();
                CompareResults();
            }

            Console.WriteLine("\nDigita 1 per eseguire un'altra operazione, digita 2 per chiudere il programma:");
            string exitChoice = Console.ReadLine();
            if (exitChoice == "1")
            {
                resultFound = false;
                constant = 0;
                unknownFactorsNumber = 0;
                Main(args);
            }
            else if (exitChoice == "2")
            {
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Comando non riconosciuto, digita qualsiasi tasto per uscire");
                Console.ReadKey();
            }
        }

        static void ComputeResults()
        {
            c = (a + b) / 2;

            //DEBUG
            //functionCenterValue = 2*c - 2; // x^3 + 2x^2 + 4
            //functionLeftExtremeValue = 2*a - 2;
            //functionRightExtremeValue = 2*b - 2;
            //DEBUG
            //functionCenterValue = Convert.ToSingle(Math.Pow(c, 3)) + 2*c + 4;
            //functionLeftExtremeValue = Convert.ToSingle(Math.Pow(a, 3)) + 2*a + 4;
            //functionRightExtremeValue = Convert.ToSingle(Math.Pow(b, 3)) + 2*b + 4;

            functionCenterValue = 0;
            functionLeftExtremeValue = 0;
            functionRightExtremeValue = 0;

            int elementsNumber = 0;

            while (elementsNumber < unknownFactorsNumber)
            {
                //DEBUG
                //Console.WriteLine("Coefficente:" + coefficents[elementsNumber] + "  Esponente:" + exponents[elementsNumber] + "  Numero c:" + c);
                functionCenterValue += coefficents[elementsNumber] * Math.Pow(c, exponents[elementsNumber]);
                functionLeftExtremeValue += coefficents[elementsNumber] * Math.Pow(a, exponents[elementsNumber]);
                functionRightExtremeValue += coefficents[elementsNumber] * Math.Pow(b, exponents[elementsNumber]);
                elementsNumber++;
            }

            elementsNumber = 0;

            functionCenterValue += constant;
            functionLeftExtremeValue += constant;
            functionRightExtremeValue += constant;

            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("\na=" + a + "  c=" + c + "  b=" + b);
            Console.WriteLine("F(a)=" + functionLeftExtremeValue + "  F(c):" + functionCenterValue + "  F(b):" + functionRightExtremeValue + "\n");
            Console.ForegroundColor = ConsoleColor.White;
        }

        static void CompareResults()
        {
            if (functionCenterValue < 0.001 && functionCenterValue > -0.001)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("Risultato trovato:" + c);
                Console.ForegroundColor = ConsoleColor.White;
                resultFound = true;
            }
            else if (functionLeftExtremeValue < 0 && functionCenterValue > 0 || functionLeftExtremeValue > 0 && functionCenterValue < 0)
            {
                b = c;
                //DEBUG
                //Console.WriteLine("Opzione 1");
            }
            else if (functionRightExtremeValue < 0 && functionCenterValue > 0 || functionRightExtremeValue > 0 && functionCenterValue < 0)
            {
                a = c;
                //DEBUG
                //Console.WriteLine("Opzione 2");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Le ipotesi non sono rispettate. Non è stato possibile eseguire l'operazione.");
                Console.ForegroundColor = ConsoleColor.White;
                resultFound = true;
            }
        }
    }
}
