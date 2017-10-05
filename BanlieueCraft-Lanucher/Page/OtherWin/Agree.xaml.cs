using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace BanlieueCraft_Lanucher
{
    /// <summary>
    /// Agree.xaml 的交互逻辑
    /// </summary>
    public partial class Agree : Window
    {
        public Agree()
        {
            InitializeComponent();
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            //MessageBox.Show("1");
            if (Sv != null && Math.Abs(Sv.VerticalOffset - (Sv.ExtentHeight - Sv.ViewportHeight)) <= 0)
            {
                CheckBox.IsEnabled = true;
            }
            else
            {
                CheckBox.IsEnabled = false;
                if (CheckBox.IsChecked != null && (bool) CheckBox.IsChecked)
                {
                    CheckBox.IsChecked = false;
                }
            }
        }
    }
}
