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
            lecimy(1,1,0);
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
        List<Point> visitedPionts = new List<Point>();
        List<int> odbyteRuchy = new List<int>();

        #region przejdz
        public void lecimy(int x, int y, int r)
        {
            Point point = new Point(x, y);
            Point pointNext = new Point(x,y);
            int ruch = r;
            
            visitedPionts.Add(point);
            odbyteRuchy.Add(r);
            int poprzedniRuch = odbyteRuchy[odbyteRuchy.Count-1];
            int sprRuch;

            switch (poprzedniRuch)
            {
                case 0:
                    sprRuch = 1;
                    pointNext = new Point(visitedPionts[visitedPionts.Count-1].X, visitedPionts[visitedPionts.Count - 1].Y + 1);
                    break;
                case 1:
                    sprRuch = 2;
                    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X, visitedPionts[visitedPionts.Count - 1].Y - 1);
                    break;
                case 2:
                    sprRuch = 3;
                    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X + 1, visitedPionts[visitedPionts.Count - 1].Y);
                    break;
                case 3:
                    sprRuch = 0;
                    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X - 1, visitedPionts[visitedPionts.Count - 1].Y);
                    break;
            }

            if (czySciana(pointNext.X, pointNext.Y, listaScian))
            {

                rysujWyjscie(point.X, point.Y, r);

                switch (r)
                {
                    case 0:
                        lecimy(point.X + 1, point.Y, r);
                        break;
                    case 1:
                        lecimy(point.X, point.Y + 1, r);
                        break;
                    case 2:
                        lecimy(point.X - 1, point.Y, r);
                        break;
                    case 3:
                        lecimy(point.X, point.Y - 1, r);
                        break;
                }
            }
            else
            {
                if (r == 0)
                    r = 1;
                else if (r == 1)
                    r = 2;
                else if (r == 2)
                    r = 3;
                else if (r == 3)
                    r = 0;
                lecimy(point.X, point.Y, r);




                //visitedPionts.Add(point);

                //if (r != 3)
                //    odbyteRuchy.Add(r + 1);
                //else
                //    odbyteRuchy.Add(0);
                //lecimy(visitedPionts[visitedPionts.Count - 1].X, visitedPionts[visitedPionts.Count - 1].Y, odbyteRuchy[odbyteRuchy.Count - 1]);
                //lecimy(point.X, point.Y, odbyteRuchy[odbyteRuchy.Count - 1]);
            }

            

            //Point pointPrev = new Point();
            //Point point = new Point();
            //int poprzedniRuch = r;

            //point.X = x;
            //point.Y = y;

            //while (!czySciana(point.X, point.Y, listaScian))
            //{
            //    rysujWyjscie(point.X, point.Y, r);
            //    ruch.Add(r);
            //    visitedPionts.Add(point);

            //    switch (r)
            //    {
            //        case 0:
            //            point.X += 1;
            //            break;
            //        case 1:
            //            point.Y += 1;
            //            break;
            //        case 2:
            //            point.X -= 1;
            //            break;
            //        case 3:
            //            point.Y -= 1;
            //            break;
            //        default:
            //            break;
            //    }
            //}
            //pointPrev = visitedPionts[visitedPionts.Count - 1];
            //poprzedniRuch = ruch[visitedPionts.Count - 1];
            //int nRuch = 0;
            //List<int> mozliweRuchy = new List<int>();
            //mozliweRuchy.Add(0);
            //mozliweRuchy.Add(1);
            //mozliweRuchy.Add(2);
            //mozliweRuchy.Add(3);
            //List<int> mr = new List<int>();
            //foreach (var item in mozliweRuchy)
            //{
            //    if (poprzedniRuch != item)
            //        nRuch = item;
            //}
            //lecimy(pointPrev.X, pointPrev.Y, nRuch);
            

                //    int poprzedniRuch = r;
                //    while (!czySciana(x, y, listaScian))
                //    {
                //        rysujWyjscie(x, y, r);

                //        switch (r)
                //        {
                //            case 0:
                //                x += 1;
                //                break;
                //            case 1:
                //                y += 1;   
                //                break;
                //            case 2:
                //                x -= 1;
                //                break;
                //            case 3:
                //                y -= 1;
                //                break;
                //            default:
                //                break;
                //        }
                //    }

                //    switch (poprzedniRuch)
                //    {
                //        case 0:
                //            lecimy(x - 1, y, 1);
                //            break;
                //        case 1:
                //            lecimy(x, y - 1, 2);
                //            break;
                //        case 2:
                //            lecimy(x + 1, y, 3);
                //            break;
                //        case 3:
                //            lecimy(x, y + 1, 0);
                //            break;
                //        default:
                //            break;
                //    }



                //rysujWyjscie(0, 1, 0); // prawo
                //rysujWyjscie(1, 1, 0); // prawo
                //rysujWyjscie(2, 1, 0); // prawo
                //rysujWyjscie(3, 1, 1); // dol
                //rysujWyjscie(3, 2, 1); // dol
                //rysujWyjscie(3, 3, 2); // lewo
                //rysujWyjscie(2, 3, 2); // lewo
                //rysujWyjscie(1, 3, 1); // dol 
                //rysujWyjscie(1, 4, 1); // dol
                //rysujWyjscie(1, 5, 1); // dol
                //rysujWyjscie(1, 6, 1); // dol
                //rysujWyjscie(1, 7, 0); // prawo
                //rysujWyjscie(2, 7, 0); // prawo
                //rysujWyjscie(3, 7, 0); // prawo
                //rysujWyjscie(4, 7, 0); // prawo
                //rysujWyjscie(5, 7, 0); // prawo
                //rysujWyjscie(6, 7, 0); // prawo
                //rysujWyjscie(7, 7, 0); // prawo
                //rysujWyjscie(7, 6, 1); // test
                //rysujWyjscie(3, 16, 2); // test
                //rysujWyjscie(2, 16, 1); // test

            }

        public void rysujWyjscie(float x, float y, int ruch)
        {
            //czySciana(x, y, listaScian);
            Pen penGreen = new Pen(Color.Green);
            Pen penRed = new Pen(Color.Red);
            drawArea.DrawLine(penGreen, skaluj2(0), skaluj2(1), skaluj2(1), skaluj2(1));
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

        public bool czySciana(float x, float y, List<string> ls)
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
            }
            
            foreach (var item in ls2)
            {
                var spl = item.Split(' ');
                float wx1 = float.Parse(spl[0].ToString());
                float wy1 = float.Parse(spl[1].ToString());
                float wx2 = float.Parse(spl[2].ToString());
                float wy2 = float.Parse(spl[3].ToString());
                if ((x == wx1 && y == wy1) || (x == wx2 && y == wy2))
                {
                    return true;
                }
                if (wx1 != wx2)
                {
                    if (x == (wx2 - 1) && y == wy1)
                    {
                        return true;
                    }
                }
                if (wy1 != wy2)
                {
                    if (x == wx1 && y == (wy2 - 1))
                    {
                        return true;
                    }
                }  
            }
            return false;

        }

        #endregion
    }
}
