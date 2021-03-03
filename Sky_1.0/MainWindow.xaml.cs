using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Sky_1._0
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow1 : Window
    {
        public MainWindow1()
        {
            InitializeComponent();
            
            m.ImageSource = new BitmapImage(new Uri(@"C:\SK\1.jpg"));
        }
        Window1 w = new Window1();
            private void Button_Click(object sender, RoutedEventArgs e)
        {
            w.Show();
            this.Close();
            w.relod();
            
        }
    }
}
