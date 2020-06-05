using System;
namespace plcode1cs
{
    class Program
    {
        struct dane
        {
            public int dyst;
            public int poprzednik;
        };

        static void wypiszDane(dane d)
        {
            if (d.dyst == int.MaxValue)
            {
                Console.Write("  -  ");
            }
            else
            {
                Console.Write("{0} | {1}", d.dyst, d.poprzednik);
            }
            Console.Write("\t");
        }

        static int addSaturate(int x, int y)
        {
            if (x == int.MaxValue || y == int.MaxValue)
                return int.MaxValue;
            if (int.MaxValue - x >= y)
            {
                return x + y;
            }
            else
            {
                return int.MaxValue;
            }
        }

        static dane[,,] FloydWarshall(int[,] macierz, int n, int k)
        {
            dane[,,] wynik = new dane[k, n, n];

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (macierz[i, j] != 0)
                    {
                        wynik[0, i, j].dyst = macierz[i, j];
                        wynik[0, i, j].poprzednik = i;
                    }
                    else
                    {
                        wynik[0, i, j].dyst = int.MaxValue;
                        wynik[0, i, j].poprzednik = -1;
                    }
                }
            }

            for (int q = 1; q < n; q++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int dyst = addSaturate(wynik[q - 1, i, q - 1].dyst, wynik[q - 1, q - 1, j].dyst);
                        if (dyst < wynik[q - 1, i, j].dyst)
                        {
                            wynik[q, i, j].dyst = dyst;
                            wynik[q, i, j].poprzednik = q - 1;
                        }
                        else
                        {
                            wynik[q, i, j].dyst = wynik[q - 1, i, j].dyst;
                            wynik[q, i, j].poprzednik = wynik[q - 1, i, j].poprzednik;
                        }
                    }
                }
            }

            return wynik;
        }

        static void Main(string[] args)
        {
            Console.Write("Ile wierzchoЕ‚kГіw ma graf?\n n = ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.Write("Ile punktГіw poЕ›rednich?\n k = ");
            int k = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj macierz:");
            int[,] macierz = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] dane = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                    macierz[i, j] = Convert.ToInt32(dane[j]);
            }
            dane[,,] wynik = FloydWarshall(macierz, n, k);
            for (int q = 0; q < k; q++)
            {
                Console.WriteLine("[ k = {0} ]", q);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        wypiszDane(wynik[q, i, j]);
                    Console.WriteLine();
                }
                Console.Write("\n\n");
            }
            Console.ReadKey();
        }
    }
}