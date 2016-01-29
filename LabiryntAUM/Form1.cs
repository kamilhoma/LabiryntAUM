using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabiryntAUM
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            drawArea = drawingArea.CreateGraphics();
        }

        
        Graphics drawArea;

        private void btDraw_Click(object sender, EventArgs e)
        {
            List<string> z = czytajDane();
            foreach (var item in z)
            {
                var spl = item.Split(' ');
                rysuj(Int32.Parse(spl[0].ToString()), Int32.Parse(spl[1].ToString()), Int32.Parse(spl[2].ToString()), z);
            }

        }

        private void btn_przejdz_Click(object sender, EventArgs e)
        {
            lecimy();
        }

        public List<string> czytajDane()
        {
            List<string> dane = new List<string>();
            string lina;

            System.IO.StreamReader file =
                new System.IO.StreamReader(@"mazeData4x4.txt");
            while ((lina = file.ReadLine()) != null)
            {
                 dane.Add(lina);
            }

            file.Close();
            return dane;

        }
        List<string> listaScian = new List<string>();

        #region rysuj
        public List<string> rysuj(int x, int y, int instrukcja, List<string> dane)
        {
            
            string last = dane.Last().ToString();
            var spl = last.Split(' ');
            int xmax = Int32.Parse(spl[0].ToString());
            int ymax = Int32.Parse(spl[1].ToString());

            Pen penBlack = new Pen(Color.Black);
            Pen penRed = new Pen(Color.Red);
            switch (instrukcja)
            {
                case 0:
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x), skaluj(y+1));
                    listaScian.Add(x + "," + y + "," + x + "," + ((int)(y + 1)).ToString());
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    listaScian.Add(x + "," + y + "," + ((int)(x + 1)).ToString() + "," + y);
                    break;
                case 1:
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x), skaluj(y+1));
                    listaScian.Add(x + "," + y + "," + x + "," + ((int)(y + 1)).ToString());
                    break;
                case 2:
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    listaScian.Add(x + "," + y + "," + ((int)(x + 1)).ToString() + "," + y);
                    break;
                case 3:
                    break;
            }
            drawArea.DrawLine(penBlack, skaluj(0), skaluj(0), skaluj(xmax+1), skaluj(0));
            drawArea.DrawLine(penBlack, skaluj(0), skaluj(0), skaluj(0), skaluj(ymax+1));
            drawArea.DrawLine(penBlack, skaluj(xmax+1), skaluj(ymax+1), skaluj(xmax+1), skaluj(0));
            drawArea.DrawLine(penBlack, skaluj(xmax + 1), skaluj(ymax + 1), skaluj(0), skaluj(ymax+1));

            drawArea.DrawLine(penRed, skaluj(0), skaluj(0), skaluj(0), skaluj(1));
            drawArea.DrawLine(penRed, skaluj(xmax+1), skaluj(ymax), skaluj(xmax+1), skaluj(ymax+1));

            //foreach (var item in listaScian)
            //{
            //    Debug.WriteLine(item.ToString());
            //}

            return listaScian;
            
        }

        public float skaluj(float x)
        {
            int s = 38;
            x = x * s;
            return x;
        }

        public float skaluj2(float x)
        {
            int s = 38;
            x = (x * s) / 2;
            return x;
        }
        #endregion

        #region przejdz
        public void lecimy()
        {
            //rysujWyjscie(0, 1, 0); // prawo
            rysujWyjscie(1, 1, 0); // prawo
            rysujWyjscie(2, 1, 0); // prawo
            rysujWyjscie(3, 1, 1); // dol
            rysujWyjscie(3, 2, 1); // dol
            rysujWyjscie(3, 3, 2); // lewo
            rysujWyjscie(2, 3, 2); // lewo
            rysujWyjscie(1, 3, 1); // dol 
            rysujWyjscie(1, 4, 1); // dol
            rysujWyjscie(1, 5, 1); // dol
            rysujWyjscie(1, 6, 1); // dol
            rysujWyjscie(1, 7, 0); // prawo
            rysujWyjscie(2, 7, 0); // prawo
            rysujWyjscie(3, 7, 0); // prawo
            rysujWyjscie(4, 7, 0); // prawo
            rysujWyjscie(5, 7, 0); // prawo
            rysujWyjscie(6, 7, 0); // prawo
            rysujWyjscie(7, 7, 0); // prawo
            rysujWyjscie(7, 6, 1); // test
            //rysujWyjscie(3, 16, 2); // test
            //rysujWyjscie(2, 16, 1); // test

        }

        public void rysujWyjscie(float x, float y, int ruch)
        {
            czySciana(x, y, listaScian);
            Pen penGreen = new Pen(Color.Green);
            Pen penRed = new Pen(Color.Red);
            switch (ruch)
            {
                case 0: // w prawo
                    drawArea.DrawLine(penGreen, skaluj2(x), skaluj2(y), skaluj2(x + 1), skaluj2(y));
                    break;
                case 1: // w dol
                    drawArea.DrawLine(penGreen, skaluj2(x), skaluj2(y), skaluj2(x), skaluj2(y + 1));
                    break;
                case 2: // w lewo
                    drawArea.DrawLine(penGreen, skaluj2(x), skaluj2(y), skaluj2(x-1), skaluj2(y));
                    break;
                case 3: // w gore
                    drawArea.DrawLine(penGreen, skaluj2(x), skaluj2(y), skaluj2(x), skaluj2(y - 1));
                    break;
            }
        }

        public void czySciana(float x, float y, List<string> ls)
        {
            List<string> ls2 = new List<string>();
            foreach (var item in ls)
            {
                var spl = item.Split(',');
                float wx1 = float.Parse(spl[0].ToString());
                float wy1 = float.Parse(spl[1].ToString());
                float wx2 = float.Parse(spl[2].ToString());
                float wy2 = float.Parse(spl[3].ToString());
                ls2.Add((wx1 * 2).ToString() + " " + (wy1 * 2).ToString() + " " + (wx2 * 2).ToString() + " " + (wy2 * 2).ToString());
                //ls2.Add((wx1).ToString() + " " + (wy1 +1).ToString());
                //ls2.Add((wx1).ToString() + " " + (wy1-1).ToString() + " " + (wx2).ToString() + " " + (wy2-1).ToString());
                //ls2.Add((wx1-1).ToString() + " " + (wy1).ToString() + " " + (wx2-1).ToString() + " " + (wy2).ToString());
            }
            foreach (var item in ls2)
            {
                Debug.WriteLine(item);
            }

            foreach (var item in ls2)
            {
                var spl = item.Split(' ');
                float wx1 = float.Parse(spl[0].ToString());
                float wy1 = float.Parse(spl[1].ToString());
                float wx2 = float.Parse(spl[2].ToString());
                float wy2 = float.Parse(spl[3].ToString());
                if ((x == wx1 && y == wy1) || (x == wx2 && y == wy2))
                    MessageBox.Show("");
                if(wx1 != wx2)
                    if (x == (wx2- 1) && y == wy1)
                        MessageBox.Show("");
                if (wy1 != wy2)
                    if (x == wx1 && y == (wy2 - 1))
                        MessageBox.Show("");
            }

        }

        #endregion
    }
}
