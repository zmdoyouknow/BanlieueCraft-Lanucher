using System;
using System.Collections.Generic;
using System.IO;
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
    /// Setting.xaml 的交互逻辑
    /// </summary>
    public partial class Setting : Window
    {
        public Setting()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            string file = Environment.CurrentDirectory;//读取路径
            string filepatch = file + @"\" + "config.ini"; //配置文件
            IniFile ini = new IniFile(file + @"\" + "config.ini");
            if (File.Exists(filepatch))
            {
                try
                {
                    RadioButton1.IsChecked = bool.Parse(ini.IniReadValue("Default", "OnClosed_Min"));
                    RadioButton2.IsChecked = bool.Parse(ini.IniReadValue("Default", "OnClosed_Close"));
                    RadioButton3.IsChecked = bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang"));
                    RadioButton4.IsChecked = bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn"));
                    checkbox1.IsChecked = bool.Parse(ini.IniReadValue("Default", "Lancher_AutoRun"));
                    checkbox2.IsChecked = bool.Parse(ini.IniReadValue("Default", "Lancher_ShortCut"));
                    RadioButton5.IsChecked = bool.Parse(ini.IniReadValue("Default", "IP_CTCC"));
                    RadioButton6.IsChecked = bool.Parse(ini.IniReadValue("Default", "IP_CUCC"));
                }
                catch
                {

                }
            }            
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string file = Environment.CurrentDirectory;//读取路径
            string filepatch = file + @"\" + "config.ini"; //配置文件
            IniFile ini = new IniFile(file + @"\" + "config.ini");
            ini.IniWriteValue("Default", "OnClosed_Min", RadioButton1.IsChecked.ToString());
            ini.IniWriteValue("Default", "OnClosed_Close", RadioButton2.IsChecked.ToString());
            ini.IniWriteValue("Default", "OnDownRoad_Mojang", RadioButton3.IsChecked.ToString());
            ini.IniWriteValue("Default", "OnDownRoad_Zhcn", RadioButton4.IsChecked.ToString());
            ini.IniWriteValue("Default", "Lancher_AutoRun", checkbox1.IsChecked.ToString());
            ini.IniWriteValue("Default", "Lancher_ShortCut", checkbox2.IsChecked.ToString());
            ini.IniWriteValue("Default", "IP_CTCC", RadioButton5.IsChecked.ToString());
            ini.IniWriteValue("Default", "IP_CUCC", RadioButton6.IsChecked.ToString());
            this.Close();
        }
    }
}
