using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project
{
    struct wynik
    {
        public int dist;
        public int prev;
    };
    class Graf
    {
        
        public int Sprawdż(int x,int y)
        {
            if (x == int.MaxValue || y == int.MaxValue)
            {
                return int.MaxValue;
            }
            if (int.MaxValue - x >= y)
            {
                return x + y;
            }
            else
            {
                return int.MaxValue;
            }
        }

        public wynik[,,] Sriednica(int[,] maczieżS, int n) //Floyd Warshall
        {
            wynik[,,] wynik = new wynik[n, n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (maczieżS[i, j] != 0)
                    {
                        wynik[i, j, 0].dist = maczieżS[i, j];
                        wynik[i, j, 0].prev = i;
                    }
                    else
                    {
                        wynik[i, j, 0].dist = int.MaxValue;
                        wynik[i, j, 0].prev = -1;
                    }
                }
            }
            for (int k = 1; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        int c = Sprawdż(wynik[i,k,k - 1].dist, wynik[k,j,k - 1].dist);
                        if ( c < wynik[i, j, k - 1].dist)
                        {
                            wynik[i, j, k].dist = c;
                            wynik[i, j, k].prev = wynik[k, j, k - 1].prev;

                        }
                        else
                        {
                            wynik[i, j, k].dist = wynik[i, j, k - 1].dist;
                            wynik[i, j, k].prev = wynik[i, j, k - 1].prev;

                        }
                    }
                }
            }
            return wynik;
        }
        public void wypisz(wynik w)
        {
            if (w.dist == int.MaxValue)
            {
                Console.WriteLine("---");
            }
            else
            {
                Console.WriteLine(w.dist + "|" + w.prev);

            }
        }
    }


    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Ile wierzchołków ma graf?\n n = ");
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Podaj macierz sąsiadstwa:");
            int[,] macierz = new int[n, n];
            for (int i = 0; i < n; i++)
            {
                string[] dane = Console.ReadLine().Split(' ');
                for (int j = 0; j < n; j++)
                    macierz[i, j] = Convert.ToInt32(dane[j]);
            }

            //int[,] macierżS =  {{ 0, 1, 1, 0 },
            //                    { 1, 0, 1, 1 },
            //                    { 1, 1, 0, 1 },
            //                    { 0, 1, 1, 0 }};

            //int[,] Macierżf =  {{ 0, 0, 0, 3, 0},
            //                    { 3, 0, 4, 1, 0},
            //                    { 0,-1, 0, 2, 0},
            //                    {-2, 5, 0, 0, 2},
            //                    { 0, 0, 1, 0, 0} };

            //int[,] MaczierzW =  {{ 0, 1, 0, 2, 5, 0},
            //                     { 1, 0, 2, 0, 5, 0},
            //                     { 0, 2, 0, 0, 1, 4},
            //                     { 2, 0, 0, 0, 3, 0},
            //                     { 5, 5, 1, 3, 0, 1},
            //                     { 0, 0, 4, 0, 1, 0} };
            Graf g = new Graf();
            // maks z najkrotszych pomiedzy 2 wierz
            wynik[,,] b = g.Sriednica(macierz, n);
            Console.WriteLine();
            wynik a = b[0, 0, n - 1];
            for (int k = 0; k < n; k++)
            {
                Console.WriteLine("[ k = {0} ]", k);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (k == (n - 1)) 
                        {
                            if (b[i,j,k].dist > a.dist)
                            {
                                a = b[i, j, k];  // maks
                            }
                        }
                        g.wypisz(b[i, j, k]);
                    }
                    Console.WriteLine();
                }
                Console.Write("\n\n");
            }
            Console.WriteLine("Średnica:");
            g.wypisz(a);
            Console.ReadKey();
        }
    }
}