using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Sky_1._0
{
    public partial class MainWindow : Window
    {
        //0--l1 1--l 2--x1 3--y1 4--xs 5--ys
        int[][] date = new int[][] { new int[20], new int[20], new int[20], new int[20], new int[20], new int[20], };
        Random ran = new Random();
        int[] cor = { 0, 0, 0, 0 };
        Uri[] imge;
        int a = 0;
        bool lj = true;
        int t;
        int i;
        public MainWindow()
        {
            InitializeComponent();
            int n = ran.Next(1, 3);
            imge = new Uri[] { new Uri(@"C:\SK\" + n.ToString() + "m.jpg"), new Uri(@"C:\SK\" + n.ToString() + "s.jpg") };
            reload();
        }
        Thickness m1 = new Thickness();
        Thickness m2 = new Thickness();
        Double h = 0;
        private async void reload()
        {
            mapim.ImageSource = new BitmapImage(imge[0]);
            skim.ImageSource = new BitmapImage(imge[1]);
            m1.Bottom = win.ActualHeight - (win.ActualWidth - win.ActualHeight * 1.4) * 0.7142857142857143;
            h = m1.Bottom;
            m1.Left = win.ActualHeight * 1.4;
            m1.Right = 0;
            m1.Top = 0;
            map.Margin = m1;
            m2.Bottom = 0;
            m2.Right = win.ActualWidth - win.ActualHeight * 1.4;
            m2.Left = 0;
            m2.Top = 0;
            sk.Margin = m2;
            await Task.Delay(10);
            c = ((win.ActualWidth - win.ActualHeight * 1.4) * 0.714) / ActualHeight;
            b = (win.ActualWidth - win.ActualHeight * 1.4) / (win.ActualHeight * 1.4);
            //StreamWriter file = new StreamWriter("D:\\TestFile.txt");
            //file.Write(" " + (win.ActualWidth - win.ActualHeight * 1.4).ToString() + " " + ((win.ActualWidth - win.ActualHeight * 1.4) * 0.7142857142857143).ToString() + " " + (win.ActualHeight * 1.4).ToString() + " " + ActualHeight.ToString());
            //file.Close();
            reload();
        }
        int click = 0;
        Line[] lines1 = new Line[20];
        int clikscount = 0;
        List<Line> lines2 = new List<Line>();
        //<TextBlock Text="12334" Background="White"  FontSize="20"/>
        Line line1 = new Line();
           private void Rectanglemap_MauseDown(object sender, MouseButtonEventArgs e)
           {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                Point p = e.GetPosition(this);
                //MessageBox.Show("Координата x=" + p.X.ToString() + " y=" +(ActualHeight- p.Y).ToString());
                if (click == 0)
                {
                    lines2.Add(new Line());
                    lines2[clikscount].X1 = p.X;
                    lines2[clikscount].Y1 = p.Y;
                    click = 1;
                    //            click = 1; MessageBox.Show("Координата x=" + p.X.ToString() + " y=" + p.Y.ToString());
                }
                else
                {
                    click = 0;
                    lines2[clikscount].X2 = p.X;
                    lines2[clikscount].Y2 = p.Y;
                    lines2[clikscount].Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    gri.Children.Add(lines2[clikscount]);
                    // MessageBox.Show("Координата x1=" + line1.X1.ToString() + " y1=" + line1.Y1.ToString()," x2 = " + line1.X2.ToString() + " y2 = " + line1.Y2.ToString());
                    if (clikscount == 19)
                    {
                        clikscount = 19;
                    }
                    else
                    {
                        clikscount = clikscount + 1;
                    }
                }
            }
            else
            {
              //  MessageBox.Show(lines2.Count.ToString());
                if (lines2.Count > 0)
                {
                    gri.Children.Remove(lines2[lines2.Count - 1]);
                    lines2.RemoveAt(lines2.Count - 1);
                    clikscount--;
                }
            }
        }
        private void Rectanglesk_MauseDown(object sender, MouseButtonEventArgs e)
        {

            if (lj == true)
            {
                lj = false;
                a = ran.Next(10, 20);
                for (int i = 0; i < a; i++)
                {
                    int xpr = -1;
                    int ypr = -1;
                    while (xpr > Convert.ToInt32(ActualHeight) || xpr < 0 || ypr > Convert.ToInt32(ActualHeight) || ypr < 0)
                    {
                        date[0][i] = ran.Next(100, 150);
                        date[1][i] = ran.Next(10, 20);
                        date[2][i] = ran.Next(-4, 4);
                        date[3][i] = ran.Next(-4, 4);
                        date[4][i] = ran.Next(0, Convert.ToInt32(ActualHeight));
                        date[5][i] = ran.Next(0, Convert.ToInt32(ActualHeight * 1.4));
                        xpr = date[0][i] * date[2][i] + date[4][i];
                        ypr = date[0][i] * date[3][i] + date[5][i];

                    }
                    //              MessageBox.Show(x1.Count.ToString());
                }
                i = 10000;
                j = 0;
                t = 0;
                anim();
            }
        }
        int j = 0;
        private async void anim()
        {
            if (t < a - 1)
            {
                if (i > date[0][t])
                {
                    t = t + 1;

                    cor[1] = date[1][t] * date[2][t] + date[4][t];
                    cor[0] = date[4][t];
                    if (date[3][t] > 0)
                    {
                        cor[3] = date[1][t] * date[3][t] + date[5][t];
                        cor[2] = date[5][t];
                        wi.Offset = -1;
                        bl.Offset = 2;
                    }
                    else
                    {
                        cor[3] = date[1][t] * date[3][t] + date[5][t];
                        cor[2] = date[5][t];
                        wi.Offset = 1;
                        bl.Offset = 0;
                    }
                    /*lines2.Add(new Line());
                    lines2[clikscount].X1 = date[4][t] * b + m1.Left;
                    lines2[clikscount].Y1 = date[5][t] * c;
                    lines2[clikscount].X2 = (date[0][t] * date[2][t] + date[2][t] * date[1][t] + date[4][t]) * b + m1.Left;
                    lines2[clikscount].Y2 = (date[0][t] * date[3][t] + date[3][t] * date[1][t] + date[5][t]) * c;
                    lines2[clikscount].Stroke = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                    gri.Children.Add(lines2[clikscount]);
                    string s = lines2[clikscount].X1.ToString() + " " + lines2[clikscount].Y1.ToString() + " " + lines2[clikscount].X2.ToString() + " " + lines2[clikscount].Y2.ToString();
                    clikscount = clikscount + 1;*/
                    //    MessageBox.Show(s);
                    // MouseDown="Rectanglemap_MauseDown" 
                    i = 0;
                    await Task.Delay(1);
                    anim();
                }
                else
                {
                    x.X1 = cor[0];
                    x.X2 = cor[1];
                    x.Y1 = cor[2];
                    x.Y2 = cor[3];
                    plus();
                    i = 1 + i;
                    await Task.Delay(1);
                    anim();
                }
            }
            else
            {
                x.X1 = 0;
                x.X2 = 0;
                x.Y1 = 0;
                x.Y2 = 0;
                int och = 0;
                for (int o = 0; o < lines2.Count; o++)
                {
                    for (int ip = 0; ip < a - 1; ip++)
                    {

                        if (lines2[o].X1 + 10 > date[4][t] * b + m1.Left && date[4][t] * b + m1.Left > lines2[o].X1 - 10 &&
                            date[5][t] * c  > lines2[o].Y1 - 10  && date[5][t] * c < lines2[o].Y1 + 10 &&
                            (date[0][t] * date[2][t] + date[2][t] * date[1][t] + date[4][t]) * b + m1.Left < lines2[o].X2 + 10 && (date[0][t] * date[2][t] + date[2][t] * date[1][t] + date[4][t]) * b + m1.Left > lines2[o].X2 - 10 &&
                            (date[0][t] * date[3][t] + date[3][t] * date[1][t] + date[5][t]) * c < lines2[o].Y2 + 10 && (date[0][t] * date[3][t] + date[3][t] * date[1][t] + date[5][t]) * c > lines2[o].Y2 - 10)
                        {
                            och = och + 1;
                        }
                    }
                    lines2[o].X1 = 0;
                    lines2[o].X2 = 0;
                    lines2[o].Y1 = 0;
                    lines2[o].Y2 = 0;
                }
                MessageBox.Show(och.ToString());
                File.WriteAllText(@"C:\SK\win1.txt", och.ToString());
                await Task.Delay(1);
                lines2.Clear();
                clikscount = 0;
                this.Close();
            }
        }
        double c = 0;
        double b = 0;
        private void plus() { if (j == 0) { cor[0] = cor[0] + date[2][t]; cor[1] = cor[1] + date[2][t]; cor[2] = cor[2] + date[3][t]; cor[3] = cor[3] + date[3][t]; } }
    }
}