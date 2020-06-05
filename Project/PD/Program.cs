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
    class Krawędź
    {
        public int b;
        public int a;
        public Krawędź(int a, int b)
        {
            this.a = a;
            this.b = b;
        }
    }
    class Graf
    {

        public List<List<Krawędź>> Maczierz(int[,] tab)
        {

            List<List<Krawędź>> lista = new List<List<Krawędź>>();
            for (int i = 0; i < tab.GetLength(0); i++)
            {
                lista.Add(new List<Krawędź>());
                for (int j = 0; j < tab.GetLength(1); j++)
                {
                    if (tab[i, j] != 0)
                    {
                        lista[i].Add(new Krawędź(j, tab[i, j]));

                    }
                }
            }
            return lista;
        }

        public wynik[,,] Sriednica(int[,] maczieżS, int n)
        {
            wynik[,,] wynik = new wynik[n, n, n];
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    wynik[i, j, 0].dist = int.MaxValue;
                    if (maczieżS[i,j] != 0)
                    {
                        wynik[i, j, 0].dist = maczieżS[i, j];
                    }
                }
            }
            for (int k = 1; k < n; k++)
            {
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        
                        if ((wynik[i, k, k - 1].dist + wynik[k, j, k - 1].dist) < wynik[i, j, k - 1].dist)
                        {
                            wynik[i, j, k].dist = (wynik[i, k, k - 1].dist + wynik[k, j, k - 1].dist);
                            
                        }
                        else
                        {
                            wynik[i, j, k].dist = wynik[i, j, k - 1].dist;
                        }
                    }
                }
            }

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
                for (int i = 1; i < n; i++)
                {
                    for (int j = 1; j < n; j++)
                    {
                        if ((wynik[i, k, k - 1].dist + wynik[k, j, k - 1].dist) < wynik[i, j, k - 1].dist)
                        {
                            wynik[i, j, k].dist = (wynik[i, k, k - 1].dist + wynik[k, j, k - 1].dist);
                            wynik[i, j, k].prev = k;
                        }
                        else
                        {
                            wynik[i, j, k].dist = wynik[i, j, k - 1].dist;
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
            int[,] macierżS =  {{ 0, 1, 1, 0 },
                               { 1, 0, 1, 1 },
                               { 1, 1, 0, 1 },
                               { 0, 1, 1, 0 }};

            int[,] Macierżf =  {{ 0, 0, 0, 3, 0},
                                { 3, 0, 4, 1, 0},
                                { 0,-1, 0, 2, 0},
                                {-2, 5, 0, 0, 2},
                                { 0, 0, 1, 0, 0} };
            Graf g = new Graf();
            
            wynik[,,] b = g.Sriednica(Macierżf, n);
            for (int k = 0; k < n; k++)
            {
                Console.WriteLine("[ k = {0} ]", k);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                        g.wypisz(b[i, j, k]);
                    Console.WriteLine();
                }
                Console.Write("\n\n");
            }
            Console.ReadKey();
        }
    }
}