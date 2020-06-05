using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        static MemoryStream ms = new MemoryStream(new byte[256], 0, 256, true, true);
        
    
        BinaryWriter bw = new BinaryWriter(ms);
        BinaryReader br = new BinaryReader(ms);
        List<Label> lLabel = new List<Label>();
        List<string> list = new List<string>();    
        private void Labels()
        {
            lLabel.Add(label1);
            lLabel.Add(label2);
            lLabel.Add(label3);
            lLabel.Add(label4);
            lLabel.Add(label5);
            lLabel.Add(label6);
            lLabel.Add(label7);
            lLabel.Add(label8);
            lLabel.Add(label9);
        }
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            socket.Connect("192.168.1.106", 2048);
            Labels();
            Task.Run(() => { while (true) ReceivePacket(); });
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {
        }
        private void label1_Click(object sender, EventArgs e)
        {
            
                Label label = (Label)sender;
                if (label.Text == "")
                {
                    if (list.Count == 0)
                    {
                        list.Add("x");
                    }
                    if (list.Last() == "x")
                    {

                        label.Text = "o";
                        list.Add("o");
                        SendPacket(label.Name, "o");

                    }
                    else
                    {

                        label.Text = "x";
                        list.Add("x");
                        SendPacket(label.Name, "x");
                    }
                    Winner();
                }
            
            
           
           
        }

        private void Winner()
        {
            //if ((label1.Text == label2.Text && label2.Text == label3.Text) || (label1.Text == label5.Text && label5.Text == label9.Text) || (label1.Text == label4.Text && label4.Text == label7.Text))
            //{
            //    MessageBox.Show("Wygrałeś!");
            //    Close();
            //}
            if (label1.Text=="o"|| label1.Text == "x") //label1
            {
                if (label2.Text==label1.Text)
                {
                    if (label3.Text == label2.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label4.Text == label1.Text)
                {
                    if (label7.Text == label4.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label5.Text == label1.Text)
                {
                    if (label9.Text == label5.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }

            if (label7.Text == "o" || label7.Text == "x") //label7
            {
                if (label8.Text == label7.Text)
                {
                    if (label8.Text == label9.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label4.Text == label7.Text)
                {
                    if (label4.Text == label1.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label5.Text == label7.Text)
                {
                    if (label3.Text == label5.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }

            if (label9.Text == "o" || label9.Text == "x") //label9
            {
                if (label8.Text == label9.Text)
                {
                    if (label8.Text == label7.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label9.Text == label6.Text)
                {
                    if (label6.Text == label3.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label5.Text == label9.Text)
                {
                    if (label1.Text == label5.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }

            if (label3.Text == "o" || label3.Text == "x") //label3
            {
                if (label3.Text == label2.Text)
                {
                    if (label2.Text == label1.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label3.Text == label6.Text)
                {
                    if (label6.Text == label9.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
                if (label5.Text == label3.Text)
                {
                    if (label7.Text == label5.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }

            if (label2.Text == "o" || label2.Text == "x") //label2
            {
                if (label2.Text == label5.Text)
                {
                    if (label5.Text == label8.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }

            if (label4.Text == "o" || label4.Text == "x") //label4
            {
                if (label4.Text == label5.Text)
                {
                    if (label5.Text == label6.Text)
                    {
                        MessageBox.Show("Wygrałeś!");
                        Close();
                        return;
                    }
                }
            }



        }
        private void SendPacket(string label,string ostatni)
        {
            ms.Position = 0;
            bw.Write(0);
            bw.Write(label);
            bw.Write(ostatni);
            socket.Send(ms.GetBuffer());          
        }
        private void ReceivePacket()
        {
            ms.Position = 0;
            socket.Receive(ms.GetBuffer());
            int code = br.ReadInt32();
            string label;
            string ostatni;
            switch(code)
            {
                case 0:
                    label = br.ReadString();
                    ostatni = br.ReadString();
                    foreach (Label l in lLabel)
                    {
                        if (l.Name == label)
                        {
                            l.Invoke(new Action(() => l.Text = ostatni));
                        }
                    }
                    list.Add(ostatni);
                   
                break;
                    
            }
           
            
        }

        
    }
}
