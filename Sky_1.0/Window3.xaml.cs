using System;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Windows;

namespace Sky_1._0
{
    /// <summary>
    /// Логика взаимодействия для Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {
        public Window3()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = i1.Text + " " + i2.Text+" ";
            MessageBox.Show("OK");
            if (s.Contains(';')==false && s.Contains('*')==false) {
                byte[] b = new byte[s.Length];
                for(int c=0;c<s.Length;c++)
                {
                    b[c]= Convert.ToByte(s[c]);
                }
                File.WriteAllBytes(@"C:\SK\win2.txt",b);
                //MessageBox.Show("3");
                this.Close();
            }
            else
            {
                MessageBox.Show("Ошибка");
            }
        }
    }
}
