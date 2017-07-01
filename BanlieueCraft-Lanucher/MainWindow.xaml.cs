using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.IO;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Reflection;
using System.Diagnostics;
using System.Net;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace BanlieueCraft_Lanucher
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine( FindJava.JavaList()[1]);
           

        }

        public string SHA1File(string fileName)
        {
            return HashFile(fileName, "SHA1");
        }
        private string HashFile(string fileName, string algName)
        {
            if (!System.IO.File.Exists(fileName))
                return string.Empty;

            System.IO.FileStream fs = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            byte[] hashBytes = HashData(fs, algName);
            fs.Close();
            return ByteArrayToHexString(hashBytes);
        }
        private byte[] HashData(System.IO.Stream stream, string algName)
        {
            System.Security.Cryptography.HashAlgorithm algorithm;
            if (algName == null)
            {
                throw new ArgumentNullException("algName 不能为 null");
            }
            if (string.Compare(algName, "sha1", StringComparison.OrdinalIgnoreCase) == 0)
            {
                algorithm = System.Security.Cryptography.SHA1.Create();
            }
            else
            {
                if (string.Compare(algName, "md5", StringComparison.OrdinalIgnoreCase) != 0)
                {
                    throw new Exception("algName 只能使用 sha1 或 md5");
                }
                algorithm = System.Security.Cryptography.MD5.Create();
            }
            return algorithm.ComputeHash(stream);
        }

        /// <summary>
        /// 字节数组转换为16进制表示的字符串
        /// </summary>
        private string ByteArrayToHexString(byte[] buf)
        {
            return BitConverter.ToString(buf).Replace("-", "").ToLower();
        }

        private delegate void Ping();

        private void Server()
        {
            var mcinfo = new MCQuery.McQuery();
            string host = "127.0.0.1";
            mcinfo.Connect(host);
            if (mcinfo.Success())
            {
                var info = mcinfo.Info();
                
               // Console.WriteLine(info.OnlinePlayers + @"/" + info.MaxPlayers);
                Label.Content = @"玩家：" + info.OnlinePlayers + @"/" + info.MaxPlayers;
            }
            else
            {
                Label.Content = "服务器维护中...";
                Label.Foreground = new SolidColorBrush(Colors.Red);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string file = Environment.CurrentDirectory;//读取路径
            string filepatch = file + @"\" + "config.ini"; //配置文件
            //IniFile ini = new IniFile(file + @"\" + "config.ini");
            if (!File.Exists(filepatch))
            {
                Setting setting = new Setting();                
                setting.ShowDialog();
            }
            Label.Dispatcher.Invoke(new Ping(Server));
        }

        private void Button_Click_1_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick1.IsChecked = true;
            ButtonClick2.IsChecked = false;
            ButtonClick3.IsChecked = false;
            ButtonClick4.IsChecked = false;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            PageContext.Navigate(new Uri("Page/Game.xaml", UriKind.Relative));
        }

        private void Button_Click_2_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick1.IsChecked = false;
            ButtonClick2.IsChecked = true;
            ButtonClick3.IsChecked = false;
            ButtonClick4.IsChecked = false;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            //PageContext.Navigate(new Uri("Page/GameVersion.xaml", UriKind.Relative));
            //PageContext.Navigate(new Uri("Page/VerDown.xaml", UriKind.Relative));
        }

        private void Button_Click_3_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick1.IsChecked = false;
            ButtonClick2.IsChecked = false;
            ButtonClick3.IsChecked = true;
            ButtonClick4.IsChecked = false;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            PageContext.Navigate(new Uri("Page/VersionDown.xaml", UriKind.Relative));
        }

        private void Button_Click_4_Click(object sender, RoutedEventArgs e)
        {
            ButtonClick1.IsChecked = false;
            ButtonClick2.IsChecked = false;
            ButtonClick3.IsChecked = false;
            ButtonClick4.IsChecked = true;
            Label1.Visibility = Visibility.Hidden;
            Label2.Visibility = Visibility.Hidden;
            //PageContext.Navigate(new Uri("Page/StartGame.xaml", UriKind.Relative));
        }
    }
}
