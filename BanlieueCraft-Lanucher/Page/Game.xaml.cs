using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// Page.xaml 的交互逻辑
    /// </summary>
    public partial class Game
    {
        public string Verlist { get; set; }
        public string Realver { get; set; }

        public Game()
        {
            InitializeComponent();
            var uvm = new UserViewModel();
            DataContext = uvm;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ArrayList arr = new ArrayList();
            var file = Environment.CurrentDirectory;//读取路径
            var filepatch = file + @"\" + "config.ini"; //配置文件
            var ini = new IniFile(filepatch);
            var btn = new Button
            {
                Height = 40,
                Margin = new Thickness(0, 0, 0, 0),
                Style = (Style) TryFindResource("Select"),
                Content = "请选择版本"
            };
            //btn.Width = 150;
            btn.Click += btn_click;
            try
            {
                Verlist = ini.IniReadValue("Default", "SelectVer");
                Realver = ini.IniReadValue("Default", "SelectRealVer");
                if (!string.IsNullOrEmpty(Verlist))
                {
                    arr.Add(Verlist);
                    version.SelectedIndex = 0;
                }
            }
            catch
            {
                if (Verlist != null)
                {
                    arr.Add(Verlist);
                }
            }
            arr.Add(btn);
            Console.WriteLine(Verlist);
            version.ItemsSource = arr;
            //version.Items.Add(btn);
        }
        private void btn_click(object sender, RoutedEventArgs e)
        {
            version.IsDropDownOpen = false;
            frame.Navigate(new Uri("/Page/GameVersion.xaml", UriKind.Relative));
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Realver);
        }
    }
}
