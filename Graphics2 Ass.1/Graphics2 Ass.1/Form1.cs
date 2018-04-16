using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Graphics2_Ass._1
{
    public partial class Form1 : Form
    {
        Timer tt = new Timer();
        ArrayList points = new ArrayList();
        ArrayList points2 = new ArrayList();
        ArrayList Edges = new ArrayList();
        int Flag_Edge = 0;
        Bitmap off_Image = null;
        int flag = 0;
        int driv = 0;
        public Form1()
        {
            InitializeComponent();
            KeyDown += new KeyEventHandler(Form1_KeyDown);
        }

        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            DB();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                //ptrv.x = (int)(ptrv.x * Math.Cos(0.1) - ptrv.y * Math.Sin(0.1));
                //ptrv.y = (int)(ptrv.x * Math.Sin(0.1) + ptrv.y * Math.Cos(0.1));

                double x = ptrv.x;
                double y = ptrv.y;
                x = (ptrv.x * Math.Cos(5 * Math.PI / 180) - ptrv.y * Math.Sin(5 * Math.PI / 180));
                y = (ptrv.y * Math.Cos(5 * Math.PI / 180) + ptrv.x * Math.Sin(5 * Math.PI / 180));
                ptrv.x = x;
                ptrv.y = y;
            }
            DB();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (flag == 1)
            {
                flag = 1;
            }
            DB();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            flag = 1;
            string S;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);
                int i = 0;
                while (!sr.EndOfStream)
                {
                    S = sr.ReadLine();
                    S.Trim();
                    //textBox1.Text = S.ToString();

                    string[] s = S.Split(',');
                    int x, y, z;
                    int.TryParse(s[0], out x);
                    int.TryParse(s[1], out y);
                    int.TryParse(s[2], out z);
                    cnode n = new cnode();
                    n.x = x;
                    n.y = y;
                    n.z = z;
                    n.dri = i;
                    points.Add(n);
                    i++;
                    //listBox1.Items.Add(S.ToString());
                }
                sr.Close();
            }
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                //textBox1.Text = ptrv.x.ToString();
                ptrv.x += this.ClientSize.Width / 2;
                cnode n = new cnode();
                n.x = ptrv.x;
                n.y = ptrv.y;
                points2.Add(n);
                listBox1.Items.Add(ptrv.x.ToString());
                listBox2.Items.Add(ptrv.y.ToString());
                listBox3.Items.Add(ptrv.z.ToString());
            }
        }
        void DrawScene(Graphics g)
        {
            Pen pe = new Pen(Color.Black);
            g.Clear(Color.Gray);
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];

                g.FillEllipse(Brushes.Black, (float)ptrv.x, (float)ptrv.y, 5, 5);
            }

            if (Flag_Edge == 1)
            {
                for (int i = 0; i < Edges.Count; i++)
                {
                    cnode ptrv = (cnode)Edges[i];
                    cnode pt = (cnode)points[(int)ptrv.x];
                    cnode pt2 = (cnode)points[(int)ptrv.y];
                    g.DrawLine(Pens.Black, (float)pt.x, (float)pt.y, (float)pt2.x, (float)pt2.y);

                    cnode ptm = (cnode)Edges[driv];
                    cnode ptt = (cnode)points[(int)ptm.x];
                    cnode ptt2 = (cnode)points[(int)ptm.y];
                    g.DrawLine(Pens.Yellow, (float)ptt.x, (float)ptt.y, (float)ptt2.x, (float)ptt2.y);
                }
            }

        }
        void DB()
        {

            if (off_Image == null)
            {
                off_Image = new Bitmap(ClientRectangle.Width,
                    ClientRectangle.Height);
            }
            Graphics g = this.CreateGraphics();
            /////////////////////////////////////
            ///	Get the Memory Graphics of the 
            ///	BakcBuffer Image,
            ///	/////////////////////////////////
            Graphics g2 = Graphics.FromImage(off_Image);

            //////////////////////////////////////
            //	call our isolated drawing routing
            //  but pass to it g2 (BackBuffer)
            //  so the rendering will be done in
            //	the memory.
            //////////////////////////////////////
            DrawScene(g2);


            ////////////////////////////////////////
            //	push our bitmap forward to the screen
            ////////////////////////////////////////
            g.DrawImage(off_Image, 0, 0);

        }

        private void button4_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                double z = ptrv.z;
                double y = ptrv.y;
                z = (ptrv.z * Math.Cos(5 * Math.PI / 180) - ptrv.y * Math.Sin(5 * Math.PI / 180));
                y = (ptrv.y * Math.Cos(5 * Math.PI / 180) + ptrv.z * Math.Sin(5 * Math.PI / 180));
                ptrv.z = z;
                ptrv.y = y;
            }
            DB();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                double z = ptrv.z;
                double x = ptrv.x;
                z = (ptrv.z * Math.Cos(5 * Math.PI / 180) + ptrv.x * Math.Sin(5 * Math.PI / 180));
                x = (ptrv.x * Math.Cos(5 * Math.PI / 180) - ptrv.z * Math.Sin(5 * Math.PI / 180));
                ptrv.z = z;
                ptrv.x = x;
            }
            DB();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];

                double x = ptrv.x;
                double y = ptrv.y;
                x = (ptrv.x * Math.Cos(5 * Math.PI / 180) - ptrv.y * Math.Sin(5 * Math.PI / 180));
                y = (ptrv.y * Math.Cos(5 * Math.PI / 180) + ptrv.x * Math.Sin(5 * Math.PI / 180));
                ptrv.x = x;
                ptrv.y = y;
            }
            DB();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                double z = ptrv.z;
                double y = ptrv.y;
                z = (ptrv.z * Math.Cos(5 * Math.PI / 180) + ptrv.y * Math.Sin(5 * Math.PI / 180));
                y = (ptrv.y * Math.Cos(5 * Math.PI / 180) - ptrv.z * Math.Sin(5 * Math.PI / 180));
                ptrv.z = z;
                ptrv.y = y;
            }
            DB();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                double z = ptrv.z;
                double x = ptrv.x;
                z = (ptrv.z * Math.Cos(5 * Math.PI / 180) + ptrv.x * Math.Sin(5 * Math.PI / 180));
                x = (ptrv.x * Math.Cos(5 * Math.PI / 180) - ptrv.z * Math.Sin(5 * Math.PI / 180));
                ptrv.z = z;
                ptrv.x = x;
            }
            DB();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            Flag_Edge = 1;
            string S;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.IO.StreamReader sr = new
                   System.IO.StreamReader(openFileDialog1.FileName);

                while (!sr.EndOfStream)
                {
                    S = sr.ReadLine();
                    S.Trim();
                    //textBox1.Text = S.ToString();

                    string[] s = S.Split(',');
                    double x, y;
                    double.TryParse(s[0], out x);
                    double.TryParse(s[1], out y);
                    cnode n = new cnode();
                    n.x = x;
                    n.y = y;
                    Edges.Add(n);
                    //listBox1.Items.Add(S.ToString());
                }
                sr.Close();
            }
            for (int i = 0; i < Edges.Count; i++)
            {
                cnode ptrv = (cnode)Edges[i];
                listBox4.Items.Add(ptrv.x.ToString());
                listBox4.Items.Add(ptrv.y.ToString());
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (driv > 0)
                driv--;
            DB();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (driv < points.Count)
                driv++;
            DB();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            cnode ptr = (cnode)Edges[driv];
            cnode pt = (cnode)points[(int)ptr.x];
            cnode pt2 = (cnode)points[(int)ptr.y];
            //double Delta = Math.Sqrt(Math.Pow((pt2.y - pt.y), 2) + Math.Pow((pt2.x - pt.x), 2) + Math.Pow((pt2.z - pt.z), 2));
            //double x = Math.Sqrt(Math.Pow((pt2.x - pt.x), 2));
            //double y = Math.Sqrt(Math.Pow((pt2.y - pt.y), 2));
            //double z = Math.Sqrt(Math.Pow((pt2.z - pt.z), 2));

            double x = pt2.x - pt.x;
            double y = pt2.y - pt.y;
            double z = pt2.z - pt.z;

            double x2 = pt.x;
            double y2 = pt.y;
            double z2 = pt.z;

            for (int i = 0; i < points.Count; i++)
            {
                cnode p1;
                p1 = (cnode)points[i];

                p1.x -= x2;
                p1.y -= y2;
                p1.z -= z2;

                points[i] = p1;
            }



            double th = Math.Atan2(y, x);
            //th = th * Math.PI / 180;

            double phi = Math.Atan2(Math.Sqrt(x * x + y * y), z);
            //phi = (phi * Math.PI / 180);

            for (int i = 0; i < points.Count; i++)
            {
                    cnode ptrv = (cnode)points[i];

                    double xx = ptrv.x;
                    double yy = ptrv.y;
                    
                    xx = ((ptrv.x * Math.Cos(-th)) - (ptrv.y * Math.Sin(-th)));
                    yy = ((ptrv.x * Math.Sin(-th)) + (ptrv.y * Math.Cos(-th)));
                    
                    ptrv.x = xx;
                    ptrv.y = yy;
                    points[i] = ptrv;                
            }




            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                
                double zz = ptrv.z;
                double xx = ptrv.x;
                
                zz = ((ptrv.x * Math.Cos(-(phi))) + (ptrv.z * Math.Sin(-(phi))));
                xx = ((ptrv.z * Math.Cos(-(phi))) - (ptrv.x * Math.Sin(-(phi))));

//                zz = ((ptrv.x * Math.Sin(-(phi))) + (ptrv.z * Math.Cos(-(phi))));
//                xx = -((ptrv.z * Math.Sin(-(phi))) + (ptrv.x * Math.Cos(-(phi))));

                ptrv.z = zz;
                ptrv.x = xx;

                points[i] = ptrv;
            }
            



           

            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];

                double xx = ptrv.x;
                double yy = ptrv.y;
                xx = ((ptrv.x * Math.Cos(0.20)) - (ptrv.y * Math.Sin(0.20)));
                yy = ((ptrv.y * Math.Cos(0.20)) + (ptrv.x * Math.Sin(0.20)));
                ptrv.x = xx;
                ptrv.y = yy;

                points[i] = ptrv;
            }



            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];
                double zz = ptrv.z;
                double xx = ptrv.x;
                zz = ((ptrv.x * Math.Cos((phi))) + (ptrv.z * Math.Sin((phi))));
                xx = ((ptrv.z * Math.Cos((phi))) - (ptrv.x * Math.Sin((phi))));

//                zz = ((ptrv.x * Math.Sin(-(phi))) + (ptrv.z * Math.Cos(-(phi))));
//                xx = -((ptrv.z * Math.Sin(-(phi))) + (ptrv.x * Math.Cos(-(phi))));

                ptrv.z = zz;
                ptrv.x = xx;

                points[i] = ptrv;
            }


            for (int i = 0; i < points.Count; i++)
            {
                cnode ptrv = (cnode)points[i];

                double xx = ptrv.x;
                double yy = ptrv.y;
                xx = ((ptrv.x * Math.Cos(th)) - (ptrv.y * Math.Sin(th)));
                yy = ((ptrv.y * Math.Cos(th)) + (ptrv.x * Math.Sin(th)));
                ptrv.x = xx;
                ptrv.y = yy;

                points[i] = ptrv;
            }

            for (int i = 0; i < points.Count; i++)
            {
                cnode p1 = new cnode();
                p1 = (cnode)points[i];
                p1.x += x2;
                p1.y += y2;
                p1.z += z2;

                points[i] = p1;
            }
            DB();
        }
    }
    class cnode
    {
        public double x, y, z, dri;
    }
}