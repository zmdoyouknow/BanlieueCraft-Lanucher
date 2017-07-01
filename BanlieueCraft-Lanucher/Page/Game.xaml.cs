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
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(new Uri("/Page/ServerPage.xaml", UriKind.Relative));
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            Frame1.Navigate(new Uri("/Page/OfflinePage.xaml", UriKind.Relative));
        }
    }
}
