using System;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using BanlieueCraft_Lanucher.Control;

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

        private void Btn_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("222222222");
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
           // Frame.Navigate(new Uri("/Page/GameVersion.xaml", UriKind.Relative));
        }
    }
}
