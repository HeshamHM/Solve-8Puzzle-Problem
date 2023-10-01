using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace _8_Puzzle
{

    public partial class Form1 : Form
    {
        int count = 0;
        bool move =false;
        Stack<Node> way;
        Random rand;
        int imove, jmove;
        Guna.UI2.WinForms.Guna2Button[,] button;
        public Form1()
        {
            InitializeComponent();
        }
       
        Guna.UI2.WinForms.Guna2Button btn(int i, int j)
        {
            Guna.UI2.WinForms.Guna2Button B = new Guna.UI2.WinForms.Guna2Button();
            B.Name = i.ToString() + " " + j.ToString();
            B.Width = (flowLayoutPanel1.Width/4)+40;
            B.Height = (flowLayoutPanel1.Height/4)+30;
            B.Click += b_Click;
            B.Parent = flowLayoutPanel1;
            B.BackColor = Color.Transparent;
            B.ForeColor = Color.White;
            B.FillColor = Color.Transparent;
            B.HoverState.BorderColor = Color.FromArgb(94, 148, 255);
            B.HoverState.FillColor = Color.FromArgb(94, 148, 255);
            B.BorderThickness = 1;
            B.BorderColor = Color.White;
            B.Font = new Font("Bell MT", 40);
            B.Enabled = false;
            return B;
        }

        void b_Click(object sender, EventArgs e)
        {
            Guna.UI2.WinForms.Guna2Button butt = (Guna.UI2.WinForms.Guna2Button)sender;
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            groupBox1.Text = "8 puzzle";
            groupBox1.Parent = panel1;
            groupBox1.BackColor = Color.Transparent;
            panel1.BackColor = Color.FromArgb(125, Color.Black);
            panel1.Parent = pictureBox1;
            flowLayoutPanel1.BackColor = Color.Transparent;
            flowLayoutPanel1.Parent = groupBox1;
            flowLayoutPanel1.ForeColor = Color.White;
            rand = new Random();
            button = new Guna.UI2.WinForms.Guna2Button[3, 3];
            // create implace buttons 3*3
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    button[i, j] = btn(i, j);
                    flowLayoutPanel1.Controls.Add(button[i, j]);
                }
            }
            int count = 0;
            bool []check= new bool[8];
            while (count < 8)
            {
                int x = rand.Next() % 3, y = rand.Next() % 3;
                if (button[x, y].Text == "")
                {
                    int r = rand.Next() % 8;
                    while (check[r])
                    {
                        r = rand.Next() % 8;
                        if (count == 8)
                            break;
                    }
                    if (!(check[r]))
                    {
                        button[x, y].Text = (r + 1).ToString();
                        check[r] = true;
                        count++;
                    }
                }
            }
                    
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void panel1_MouseHover(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel1_MouseLeave(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(move)
            {
                if (way.Count > 0)
                {
                    if (count == 5)
                    {
                        button[imove, jmove].FillColor = Color.Green;
                        button[way.Peek().x, way.Peek().y].FillColor = Color.Green;
                        button[imove, jmove].Enabled=true;
                        button[way.Peek().x, way.Peek().y].Enabled = true;
                    
                    }
                    else if(count==10)
                    {
                        count = 0;
                        button[imove, jmove].Text = button[way.Peek().x, way.Peek().y].Text;
                        button[way.Peek().x, way.Peek().y].Text = "";
                        button[imove, jmove].FillColor = Color.Transparent;
                        button[way.Peek().x, way.Peek().y].FillColor = Color.Transparent;
                        button[imove, jmove].FillColor = Color.Transparent;
                        button[way.Peek().x, way.Peek().y].FillColor = Color.Transparent;
                        button[imove, jmove].Enabled = false;
                        button[way.Peek().x, way.Peek().y].Enabled = false;
                        imove = way.Peek().x;
                        jmove = way.Peek().y;
                        way.Pop();
                    }

                    
                    count++;
                    

                }
            
                else
                    move = false;
                
            }
        }

        private void guna2Button5_Click(object sender, EventArgs e)
        {
            Application.Restart();
        }

        private void guna2Button4_Click(object sender, EventArgs e)
        {
            Graph gp = new Graph(3);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (button[i, j].Text == "")
                    {
                        imove = i;
                        jmove = j;
                        gp.Build(i, j, 0);
                    }
                    else

                        gp.Build(i, j, int.Parse(button[i, j].Text));
            way = new Stack<Node>();

            if (gp.solve())
            {

                way = gp.ASTAR();
                if (way.Count == 0)
                    MessageBox.Show("Already solved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                {
                    MessageBox.Show("Solved Number of Moves:" + way.Count + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    move = true;
                }
            }
            else
                MessageBox.Show("can not be solved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            Graph gp = new Graph(3);
            for (int i = 0; i < 3; i++)
                for (int j = 0; j < 3; j++)
                    if (button[i, j].Text == "")
                    {
                        imove = i;
                        jmove = j;
                        gp.Build(i, j, 0);
                    }
                    else

                        gp.Build(i, j, int.Parse(button[i, j].Text));
            way = new Stack<Node>();

                if (gp.solve())
                {

                    way = gp.BFS();
                    if (way.Count == 0)
                    MessageBox.Show("Already solved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    {
                        MessageBox.Show("Solved Number of Moves:" + way.Count + "", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        move = true;
                    }
                }
                else
                    MessageBox.Show("can not be solved", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
    }
}
