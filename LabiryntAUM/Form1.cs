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
                new System.IO.StreamReader(@"mazeData16x16.txt");
            Point koniec = new Point(8,7);
            //Point koniec = new Point(16,15);

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
                    listaScian.Add(x*2 + "," + y*2 + "," + x*2 + "," + ((int)(y + 1)*2).ToString());
                    //listaScian.Add(x + "," + y + "," + x + "," + ((int)(y + 1)).ToString());
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    listaScian.Add(x*2 + "," + y * 2 + "," + ((int)(x + 1) * 2).ToString() + "," + y * 2);
                    //listaScian.Add(x + "," + y + "," + ((int)(x + 1)).ToString() + "," + y);
                    break;
                case 1:
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x), skaluj(y+1));
                    listaScian.Add(x*2 + "," + y * 2 + "," + x * 2 + "," + ((int)(y + 1) * 2).ToString());
                    //listaScian.Add(x + "," + y + "," + x + "," + ((int)(y + 1)).ToString());
                    break;
                case 2:
                    drawArea.DrawLine(penBlack, skaluj(x), skaluj(y), skaluj(x+1), skaluj(y));
                    listaScian.Add(x*2 + "," + y * 2 + "," + ((int)(x + 1) * 2).ToString() + "," + y * 2);
                    //listaScian.Add(x + "," + y + "," + ((int)(x + 1)).ToString() + "," + y);
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

            Point point = new Point();
            Point point2 = new Point();
            foreach (var item in listaScian)
            {
                var split = item.Split(',');
                int x1 = int.Parse(split[0].ToString());
                int y1 = int.Parse(split[1].ToString());
                int x2 = int.Parse(split[2].ToString());
                int y2 = int.Parse(split[3].ToString());

                if (x2 == x1 && y2 != y1)
                {
                    x = x1;
                    y = y1 + 1;
                }
                point.X = x;
                point.Y = y;

                if (x2 != x1 && y2 == y1)
                {
                    point2.X = x1 + 1;
                    point2.Y = y1;
                }

                
            }
            listaScian.Add(point.X.ToString() + "," +point.Y.ToString() + ",0,0");
            listaScian.Add(point2.X.ToString() + "," + point2.Y.ToString() + ",0,0");

            zbudujKolizje(listaScian);
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

            if (point.X == 31 && point.Y == 31)
            {
                rysujWyjscie(point.X, point.Y, 0);
                MessageBox.Show("Koniec :)");
            }
            else {
                //jesli po prawej, z prodzu i po lewej jest sciana - idz do tylu
                if (sprawdzWPrawo(point, r) && sprawdzDoPrzodu(point, r) && sprawdzWLewo(point, r))
                {
                    switch (r)
                    {
                        case 0:
                            rysujWyjscie(point.X, point.Y, 2);
                            lecimy(point.X - 1, point.Y, 2);
                            break;
                        case 1:
                            rysujWyjscie(point.X, point.Y, 3);
                            lecimy(point.X, point.Y - 1, 3);
                            break;
                        case 2:
                            rysujWyjscie(point.X, point.Y, 0);
                            lecimy(point.X + 1, point.Y, 0);
                            break;
                        case 3:
                            rysujWyjscie(point.X, point.Y, 1);
                            lecimy(point.X, point.Y + 1, 1);
                            break;
                    }
                }
                //jesli po prawej jest sciana i z przodu jej nie ma - idz do porzodu
                if (sprawdzWPrawo(point, r) && !sprawdzDoPrzodu(point, r))
                {
                    switch (r)
                    {
                        case 0:
                            rysujWyjscie(point.X, point.Y, r);
                            lecimy(point.X + 1, point.Y, r);
                            break;
                        case 1:
                            rysujWyjscie(point.X, point.Y, r);
                            lecimy(point.X, point.Y + 1, r);
                            break;
                        case 2:
                            rysujWyjscie(point.X, point.Y, r);
                            lecimy(point.X - 1, point.Y, r);
                            break;
                        case 3:
                            rysujWyjscie(point.X, point.Y, r);
                            lecimy(point.X, point.Y - 1, r);
                            break;
                    }

                }

                //jesli z prawej nie ma sciany - idz w prawo
                if (!sprawdzWPrawo(point, r))
                {
                    switch (r)
                    {
                        case 0:
                            rysujWyjscie(point.X, point.Y, 1);
                            lecimy(point.X, point.Y + 1, 1);
                            break;
                        case 1:
                            rysujWyjscie(point.X, point.Y, 2);
                            lecimy(point.X - 1, point.Y, 2);
                            break;
                        case 2:
                            rysujWyjscie(point.X, point.Y, 3);
                            lecimy(point.X, point.Y - 1, 3);
                            break;
                        case 3:
                            rysujWyjscie(point.X, point.Y, 0);
                            lecimy(point.X + 1, point.Y, 0);
                            break;
                    }
                }

                //jesli po prawej jest sciana i z przodu jest sciana - idz w lewo
                if (sprawdzWPrawo(point, r) && sprawdzDoPrzodu(point, r))
                {
                    switch (r)
                    {
                        case 0:
                            rysujWyjscie(point.X, point.Y, 3);
                            lecimy(point.X, point.Y - 1, 3);
                            break;
                        case 1:
                            rysujWyjscie(point.X, point.Y, 0);
                            lecimy(point.X + 1, point.Y, 0);
                            break;
                        case 2:
                            rysujWyjscie(point.X, point.Y, 1);
                            lecimy(point.X, point.Y + 1, 1);
                            break;
                        case 3:
                            rysujWyjscie(point.X, point.Y, 2);
                            lecimy(point.X - 1, point.Y, 2);
                            break;
                    }
                }
            }

            #region spierdolina
            //Point point = new Point(x, y);
            //Point pointNext = new Point(x,y);
            //int ruch = r;

            //visitedPionts.Add(point);
            //odbyteRuchy.Add(r);
            //int poprzedniRuch = odbyteRuchy[odbyteRuchy.Count-1];

            //switch (poprzedniRuch)
            //{
            //    //case 0:
            //    //    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X, visitedPionts[visitedPionts.Count - 1].Y + 1);
            //    //    break;
            //    //case 1:
            //    //    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X, visitedPionts[visitedPionts.Count - 1].Y - 1);
            //    //    break;
            //    //case 2:
            //    //    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X + 1, visitedPionts[visitedPionts.Count - 1].Y);
            //    //    break;
            //    //case 3:
            //    //    pointNext = new Point(visitedPionts[visitedPionts.Count - 1].X - 1, visitedPionts[visitedPionts.Count - 1].Y);
            //    //    break;
            //}

            //if (czySciana(pointNext.X, pointNext.Y, listaScian))
            //{

            //    rysujWyjscie(point.X, point.Y, r);

            //    switch (r)
            //    {
            //        case 0:
            //            point.X += 1;
            //            //rysujWyjscie(point.X + 1, point.Y, r);
            //            break;
            //        case 1:
            //            point.Y += 1;
            //            //rysujWyjscie(point.X, point.Y + 1, r);
            //            break;
            //        case 2:
            //            point.X -= 1;
            //            //rysujWyjscie(point.X - 1, point.Y, r);
            //            break;
            //        case 3:
            //            point.Y -= 1;
            //            //rysujWyjscie(point.X, point.Y - 1, r);
            //            break;
            //    }
            //    visitedPionts.Add(point);
            //}
            //else
            //{
            //    switch (r)
            //    {
            //        case 0:
            //            r = 1;
            //            break;
            //        case 1:
            //            r = 2;
            //            break;
            //        case 2:
            //            r = 3;
            //            break;
            //        case 3:
            //            r = 0;
            //            break;
            //    }
            //}

            //lecimy(point.X, point.Y, r);
            #endregion
        }
        //sprawdz czy po lewej jest sciana
        public bool sprawdzWLewo(Point point, int ruch)
        {
            switch (ruch)
            {
                case 0:
                    point.Y -= 1;
                    break;
                case 1:
                    point.X += 1;
                    break;
                case 2:
                    point.Y += 1;
                    break;
                case 3:
                    point.X -= 1;
                    break;
            }
            if (czySciana(point.X, point.Y, zbudujKolizje(listaScian)))
            {
                return true;
            }
            else {
                return false;
            }
        }

        //sprawdz czy po prawej stronie jest sciana
        public bool sprawdzWPrawo(Point point, int ruch)
        {
            switch (ruch)
            {
                case 0:
                    point.Y += 1;
                    break;
                case 1:
                    point.X -= 1;
                    break;
                case 2:
                    point.Y -= 1;
                    break;
                case 3:
                    point.X += 1;
                    break;
            }
            if (czySciana(point.X, point.Y, zbudujKolizje(listaScian)))
            {
                return true;
            }
            else {
                return false;
            }
        }

        //sprawdz czy z przodu jest sciana
        public bool sprawdzDoPrzodu(Point point, int ruch)
        {
            switch (ruch)
            {
                case 0:
                    point.X += 1;
                    break;
                case 1:
                    point.Y +=1 ;
                    break;
                case 2:
                    point.X -= 1;
                    break;
                case 3:
                    point.Y -= 1;
                    break;
            }
            if (czySciana(point.X, point.Y, zbudujKolizje(listaScian)))
            {
                return true;
            }
            else {
                return false;
            }
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

        public bool czySciana(float x, float y, List<Point> lp)
        {
            foreach (var item in lp)
            {
                if (item.X == x && item.Y == y)
                    return true;
            }
            return false;
        }

        //public bool czySciana(float x, float y, List<string> ls)
        //{
        //    List<string> ls2 = new List<string>();

        //    for (int i = 0; i <=8 ; i++)
        //    {
        //        ls2.Add(0.ToString() + " " + i.ToString() + " " + 0.ToString() + " " + i.ToString());
        //        ls2.Add(8.ToString() + " " + i.ToString() + " " + 8.ToString() + " " + i.ToString());
        //        ls2.Add(i.ToString() + " " + 0.ToString() + " " + i.ToString() + " " + 0.ToString());
        //        ls2.Add(i.ToString() + " " + 8.ToString() + " " + i.ToString() + " " + 8.ToString());
        //    }

        //    foreach (var item in ls)
        //    {
        //        var spl = item.Split(',');
        //        float wx1 = float.Parse(spl[0].ToString());
        //        float wy1 = float.Parse(spl[1].ToString());
        //        float wx2 = float.Parse(spl[2].ToString());
        //        float wy2 = float.Parse(spl[3].ToString());
        //        ls2.Add((wx1 * 2).ToString() + " " + (wy1 * 2).ToString() + " " + (wx2 * 2).ToString() + " " + (wy2 * 2).ToString());
        //    }
            
        //    foreach (var item in ls2)
        //    {
        //        var spl = item.Split(' ');
        //        float wx1 = float.Parse(spl[0].ToString());
        //        float wy1 = float.Parse(spl[1].ToString());
        //        float wx2 = float.Parse(spl[2].ToString());
        //        float wy2 = float.Parse(spl[3].ToString());
        //        if ((x == wx1 && y == wy1) || (x == wx2 && y == wy2))
        //        {
        //            return true;
        //        }
        //        if (wx1 != wx2)
        //        {
        //            if (x == (wx2 - 1) && y == wy1)
        //            {
        //                return true;
        //            }
        //        }
        //        if (wy1 != wy2)
        //        {
        //            if (x == wx1 && y == (wy2 - 1))
        //            {
        //                return true;
        //            }
        //        }  
        //    }
        //    return false;

        //}

        public List<Point> zbudujKolizje(List<string> listaScian)
        {
            List<Point> punktyKolizyjne = new List<Point>();

            foreach (var item in listaScian)
            {
                var spl = item.Split(',');
                float wx1 = float.Parse(spl[0]);
                float wy1 = float.Parse(spl[1]);
                float wx2 = float.Parse(spl[2]);
                float wy2 = float.Parse(spl[3]);
                Point point1 = new Point((int)wx1, (int)wy1);
                Point point2 = new Point((int)wx2, (int)wy2);

                punktyKolizyjne.Add(point1);
                punktyKolizyjne.Add(point2);
            }

            Point p1 = new Point();
            Point p2 = new Point();
            Point p3 = new Point();
            Point p4 = new Point();
            for (int i = 0; i <= 32; i++)
            {
                p1.X = 0;
                p1.Y = i;
                p2.X = i;
                p2.Y = 0;
                p3.X = 32;
                p3.Y = i;
                p4.X = i;
                p4.Y = 32;
                punktyKolizyjne.Add(p1);
                punktyKolizyjne.Add(p2);
                punktyKolizyjne.Add(p3);
                punktyKolizyjne.Add(p4);
            }
            return punktyKolizyjne;
        }

        #endregion
    }
}
