using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using BanlieueCraft_Lanucher.Control;
using System.IO;
using System.Linq;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// Page.xaml 的交互逻辑
    /// </summary>
    public partial class Game
    {
        public Game()
        {
            InitializeComponent();
            var uvm = new UserViewModel();
            DataContext = uvm;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var theFolder = new DirectoryInfo(Environment.CurrentDirectory + @"\.minecraft\versions");
            var dfolder = theFolder.GetDirectories("1.12",SearchOption.TopDirectoryOnly);
            foreach (var dname in dfolder)
            {
                if (dname != null)
                {
                    Image.Tag = dname.Name;
                    //Console.WriteLine(dname.Name);
                }
                else
                {
                    Image.Tag = "未找到指定版本";
                }
            }
        }
    }
}
