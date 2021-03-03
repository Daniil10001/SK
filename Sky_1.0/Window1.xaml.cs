using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media.Imaging;

namespace Sky_1._0
{

    /// <summary>
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        bool b = false;
        public List<string> dat = new List<string>();
        List<Tuple<Nullable<int>, string>> scores = new List<Tuple<Nullable<int>, string>>();
        public Window1() // уинциализация окна
        {
           
             InitializeComponent();
            m.ImageSource = new BitmapImage(new Uri(@"C:\SK\1.jpg"));
            t1.Text = "Для начала тестирования нужно войти\nили создать аккаунт. Потом для начала\nтеста нажмите кнопку 'ТЕСТ',\nа для выхода из аккаунта н" +
               "ажмите \n'ЗАКРЫТЬ'. Для закрытия приложения \nнажмите на крестик справа \nвверху окна.\nУдачной игры.";
            Directory.CreateDirectory(@"C:\SK");
            string s = "";
            File.Create(@"C:\SK\win1.txt");
            FileStream fs = Filen();
           byte[] b = new byte[1024];
           UTF8Encoding temp = new UTF8Encoding();
           while (fs.Read(b, 0, b.Length) > 0)
           {
               s = s + temp.GetString(b);
           }
           foreach (string np in s.Split(';')) { dat.Add(np); }
            dat.RemoveAt(dat.Count-1);
            fs.Close();
            if (File.Exists(@"C:\SK\score.txt") == false)
            {
                string n = "";
                for (int j=0; j < dat.Count(); j++) { n = n + "0,"; }
                File.WriteAllText(@"C:\SK\score.txt", n);
            }
            pr = false;


        }
        public FileStream Filen()
        {
            //проверяем наличие файла с данными пользователей(логины и пароли)
            if (File.Exists(@"C:\SK\dat.txt") == true)
            {
                FileStream fl = File.OpenRead(@"C:\SK\dat.txt");
                b = true;
                return fl;
            }
            else
            {
                File.WriteAllText(@"C:\SK\dat.txt", "");//если нет создаем новый
                FileStream fl = File.OpenRead(@"C:\SK\dat.txt");
                b = false;
                return fl;
            }
           
        }
        bool pr = false;
        public async void relod()//проверка га закрытие окна
        {
            if (this.IsLoaded == false) {Environment.Exit(0); }
            await Task.Delay(500);
            if (pr == false)
            {
                string s = "";
                FileStream fl = File.OpenRead(@"C:\SK\score.txt");
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding();
                while (fl.Read(b, 0, b.Length) > 0)
                {
                    s = s + temp.GetString(b);
                }
                fl.Close();
                scores.Clear();
                string[] sc = new string[dat.Count()];
                sc = s.Split(',');
                for (int i = 0; i < dat.Count; i++)
                {
                    scores.Add(new Tuple<Nullable<int>, string>(Convert.ToInt32(sc[i]), dat[i].Split('*')[0]));
                }
                scores.Sort();
                string txt = "Очки Игроков: \n";
                int y = 0;
                for (int i = scores.Count() - 1; i >= 0; i--)
                {
                    y++;
                    txt = txt + y.ToString() + " : " + scores[i].Item2.ToString() + " - " + scores[i].Item1.ToString() + "\n";
                }
                tab.Text = txt;
                pr = true;
            }
            relod();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Window3 windo3 = new Window3();
            if (truac == false)
            {
                    acc = false;
                    windo3.Show();
                    rest(windo3);
            }
            else
            {
                const string message =
                        "Вы хотите прекратить наблюдения?";
                const string caption = "Закрыть?";
                var result = MessageBox.Show(message, caption,
                                             MessageBoxButton.YesNo);

                // If the no button was pressed ...
                if (result == MessageBoxResult.Yes)
                {
                    // cancel the closure of the form.
                    truac = false;
                    bb.Content = "Создать";
                    ab.Content = "Войти";
                    win3.Title = "Бассейн";
                }
            }
        }

        public string prov(string t) //проверка данных и вход в аккаунт
        {
          //  MessageBox.Show("66");
            string t1 = t.Split(' ')[0];
            string t2 = t.Split(' ')[1];
         //   MessageBox.Show(t);
            bool ret = false;
            int num = -1;

            if (acc == true)
            {
                for (int i = 0; i < dat.Count(); i++)
                {
                    //MessageBox.Show("66");
                    if (dat[i].Split('*')[0] == t1)
                    {
                        num = i;
                        if (dat[i].Split('*')[1] == t2)
                        {
                            ret = true;
                            acnum = i;
                            num = i;
                            sav();
                            MessageBox.Show("Вы успешнао вошли");
                            win3.Title = t1;
                            
                        }

                    }
                }
                if (ret == false && num==-1)
                {
                    MessageBox.Show("Неверный пароль.");
                }
                if (num == -1)
                {
                    MessageBox.Show("Такого пользователя нет.");
                }
            }
            else
            {
                bool g = false;
             //   MessageBox.Show("66");
                for (int i = 0; i < dat.Count(); i++)
                {
                    
                    if (dat[i].Split('*')[0] == t1)
                    {
                        g = true;
                        MessageBox.Show("Ошибка. Такой пользователь уже существует.");
                        break;
                    }
                }
                //MessageBox.Show("55");
                if (g == false) 
                {
                    FileStream f = File.OpenRead(@"C:\SK\dat.txt");
                    string s = "";
                    byte[] b = new byte[1024];
                    UTF8Encoding temp = new UTF8Encoding();
                    while (f.Read(b, 0, b.Length) > 0)
                    {
                        s = s + temp.GetString(b);
                    }
                    f.Close();
                    acnum=dat.Count();
                    dat.Add(t1 + "*" + t2); File.AppendAllText(@"C:\SK\dat.txt", (t1+"*"+t2+";")); 
                    File.AppendAllText(@"C:\SK\score.txt", ("0,")); sav(); 
                    win3.Title = t1;
                    pr = false;
                }
            }
            return t1;
        }
        
        public void sav() { truac = true; ab.Content = "Тест"; bb.Content = "Закрыть"; }
        public string save() 
        {

            FileStream f = File.OpenRead(@"C:\SK\win2.txt");
            string s="";
            byte[] b = new byte[1024];
            UTF8Encoding temp = new UTF8Encoding();
            while (f.Read(b, 0, b.Length) > 0)
            {
                s = s + temp.GetString(b);
            }
            f.Close();
            return s;
        } 
        public bool acc = false;
        public int acnum = 0;
        private void Button_Click_1(object sender, RoutedEventArgs e)//обработка одноой из кнопок
        {
            Window3 windo3 = new Window3();
            if (truac == false)
            {
                
                    acc = true;
                    windo3.Show();
                    rest(windo3);
                
            }
            else
            {
                MainWindow mm = new MainWindow();
                pr = true;
                mm.Show();
                rest1(mm);
            }
        }
        public async void rest1(MainWindow l) { //сохранение данных
            if (l.IsLoaded == false) 
            {
                FileStream f = File.OpenRead(@"C:\SK\win1.txt");
                string s = "";
                byte[] b = new byte[1024];
                UTF8Encoding temp = new UTF8Encoding();
                while (f.Read(b, 0, b.Length) > 0)
                {
                    s = s + temp.GetString(b);
                }
                f.Close();

                 f = File.OpenRead(@"C:\SK\score.txt");
                string s1 = "";
                 b = new byte[1024];
                 temp = new UTF8Encoding();
                while (f.Read(b, 0, b.Length) > 0)
                {
                    s1 = s1 + temp.GetString(b);
                }
                f.Close();
                string[] sr = s1.Split(',');
                if (s[0] != ' ')
                {
                    sr[acnum] = (Convert.ToInt32(s) + Convert.ToInt32(sr[acnum])).ToString();
                }
                s = "";
                for (int i = 0; i < sr.Length-1; i++) { s = s + sr[i] + ","; }
                File.WriteAllText(@"C:\SK\score.txt", (s));
                File.WriteAllText(@"C:\Sk\win1.txt", " ");
                pr = false; 
            } 
            else { pr = true; await Task.Delay(5); rest1(l); } }
        Random randf = new Random();
        
        public async void rest(Window3 l) { if (l.IsLoaded == false && save()!="") {
                prov(save());} else { await Task.Delay(randf.Next(10,100)); rest(l);} }
        public bool truac = false;
    }
}
