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
            lecimy(czytajDane());
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

        #region rysuj
        public void rysuj(int x, int y, int instrukcja, List<string> dane)
        {
            string last = dane.Last().ToString();
            var spl = last.Split(' ');
            int xmax = Int32.Parse(spl[0].ToString());
            int ymax = Int32.Parse(spl[1].ToString());

            Pen pen = new Pen(Color.Black);
            Pen pen2 = new Pen(Color.Red);
            switch (instrukcja)
            {
                case 0:
                    drawArea.DrawLine(pen, skaluj(x), skaluj(y), skaluj(x), skaluj(y+1));
                    drawArea.DrawLine(pen, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    break;
                case 1:
                    drawArea.DrawLine(pen, skaluj(x), skaluj(y), skaluj(x), skaluj(y+1));
                    break;
                case 2:
                    drawArea.DrawLine(pen, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    break;
                case 3:
                    break;
            }
            drawArea.DrawLine(pen, skaluj(0), skaluj(0), skaluj(xmax+1), skaluj(0));
            drawArea.DrawLine(pen, skaluj(0), skaluj(0), skaluj(0), skaluj(ymax+1));
            drawArea.DrawLine(pen, skaluj(xmax+1), skaluj(ymax+1), skaluj(xmax+1), skaluj(0));
            drawArea.DrawLine(pen, skaluj(xmax + 1), skaluj(ymax + 1), skaluj(0), skaluj(ymax+1));

            drawArea.DrawLine(pen2, skaluj(0), skaluj(0), skaluj(1), skaluj(0));
            drawArea.DrawLine(pen2, skaluj(xmax+1), skaluj(ymax), skaluj(xmax+1), skaluj(ymax+1));
        }

        public float skaluj(float x)
        {
            int s = 38;
            x = x * s;
            return x;
        }
        #endregion

        #region przejdz
        public void lecimy(List<string> dane)
        {
            foreach (var item in dane)
            {
                //Debug.WriteLine(item.ToString());
            }

            bool czyMoge = false;

        }

        #endregion
    }
}
