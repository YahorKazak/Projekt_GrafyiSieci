using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Średnica
{
    
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string word = "";
            int newj = 0;
            int c = 0;
            int l = 0;
            int czy = 0;
            int n = Convert.ToInt32(text_box.Text);
            int[,] macierz = new int[n, n];
            string dane = text_box1.Text;
            for (int i = 0; i < n; i++)
            {
               
                for (int j = 0; j < n; j++)
                {
                    czy = 0;
                    while (czy == 0)
                    {
                        if (i == n-1 && j == n-1)
                        {
                            l= Convert.ToInt32(dane[dane.Length - 1]);
                            macierz[i, j] = l - 48;
                            czy = 1;
                        }
                        else
                        {
                            if (dane[newj] >= '0' && dane[newj] <= '9' || dane[newj] == '-')
                            {
                                word = word + dane[newj];
                            }
                            else
                            {
                                if (word != "")
                                {
                                    c = Convert.ToInt32(word);
                                    macierz[i, j] = c;
                                    czy = 1;
                                    word = "";
                                }

                            }
                            newj++;
                        }
                    }
                }
            }
            Graf g = new Graf();
            int początek = 0;
            int koniec = 0;
            string wypiszConsole = "";  
            wynik[,,] b = g.Sriednica(macierz,n);
            Console.WriteLine();
            wynik a = b[0, 0, n - 1];
            for (int k = 0; k < n; k++)
            {
                wypiszConsole = wypiszConsole + System.Environment.NewLine;
                wypiszConsole = wypiszConsole  + "k=" + k;
                Console.WriteLine("[ k = {0} ]", k);
                for (int i = 0; i < n; i++)
                {
                    for (int j = 0; j < n; j++)
                    {
                        if (k == (n - 1))
                        {
                            if (b[i, j, k].dist > a.dist)
                            {
                                a = b[i, j, k];
                                początek = i;
                                koniec = j;
                            }
                        }
                        g.wypisz(b[i, j, k]);
                        wypiszConsole = wypiszConsole + System.Environment.NewLine;
                        wypiszConsole = wypiszConsole + b[i, j, k].dist + "|" + b[i, j, k].prev;
                    }
                    Console.WriteLine();
                    wypiszConsole = wypiszConsole + System.Environment.NewLine;

                }
                wypiszConsole = wypiszConsole + System.Environment.NewLine;
                Console.Write("\n\n");
            }
            text_box2.Text = wypiszConsole;
            text_box3.Text = "Średnica: " + a.dist + "|" + a.prev + Environment.NewLine + "Początkowy wierzchołek: " + początek + Environment.NewLine + "Końcowy wierzchołek: " + koniec;

        }
         
        private void textBox1_TextChanged_1(object sender, EventArgs e)
        {
        }

        private void button2_Click(object sender, EventArgs e)
        {
            text_box.Text = null;
            text_box1.Text = null;
            text_box2.Text = null;
            text_box3.Text = null;
        }

        private void textBox1_TextChanged_2(object sender, EventArgs e)
        {

        }

        private void text_box3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
    struct wynik
    {
        public int dist;
        public int prev;
    };
    class Graf
    {

        public int Sprawdż(int x, int y)
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

        public wynik[,,] Sriednica(int[,] maczieżS, int n)
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
                        int c = Sprawdż(wynik[i, k, k - 1].dist, wynik[k, j, k - 1].dist);
                        if (c < wynik[i, j, k - 1].dist)
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
}
