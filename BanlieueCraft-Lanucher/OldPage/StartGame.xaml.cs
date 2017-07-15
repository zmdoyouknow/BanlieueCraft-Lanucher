using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// StartGame.xaml 的交互逻辑
    /// </summary>
    public partial class StartGame
    {
        public StartGame()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.Button btn = sender as System.Windows.Controls.Button;
            TextBlock txt = sender as TextBlock;
            if (btn.Name == "button")
            {
                MessageBox.Show(btn.Tag.ToString());
            }
        }

        public class LvData
        {
            public string ver { get; set; }
            public string version { get; set; }
            public BitmapImage ImageSource { get; set; }
        }
        PageInfo<LvData> pa;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Listview.Items.Clear();
            var imagelist = new List<LvData>();
            var dirPath = @"I:\.minecraft\versions";
            var theFolder = new DirectoryInfo(dirPath);
            var dfolder = theFolder.GetDirectories();
            BitmapImage images = new BitmapImage(new Uri("/BanlieueCraft-Lanucher;component/Resources/mc.png", UriKind.Relative));
            images.DecodePixelWidth = 154;
            images.DecodePixelHeight = 198;
            foreach (var dname in dfolder)
            {
                Console.WriteLine(dname.Name);
                string sb;
                
                if (dname.Name.Contains("-"))
                {
                    string op = "OptiFine";
                    string fog = "Forge";
                    string lit = "LiteLoader";
                    if (dname.Name.ToLower().IndexOf(op.ToLower()) >-1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        sb = sa[0] + "OptiFine版";
                    }
                    else if (dname.Name.ToLower().IndexOf(fog.ToLower()) > -1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);                        
                        if (dname.Name.ToLower().IndexOf(lit.ToLower()) > -1)
                        {                            
                            sb = sa[0] + "LiteLoader-Forge版";
                        }
                        else
                        {
                            sb = sa[0] + "Forge版";
                        }
                    }                    
                    else if (dname.Name.ToLower().IndexOf(lit.ToLower()) > -1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        if (dname.Name.ToLower().IndexOf(fog.ToLower()) > -1)
                        {
                            sb = sa[0] + "LiteLoader-Forge版";
                        }
                        else
                        {
                            sb = sa[0] + "LiteLoader版";
                        }
                    }
                    else
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        sb = sa[0];
                    }
                }
                else
                {
                    sb = dname.Name;
                }
                imagelist.Add(new LvData() { ver = sb, ImageSource = images, version = dname.Name});
            }

            Console.WriteLine(imagelist.Count);

            //imagelist.Insert(0, new LvData() { ver = "1.11.2", ImageSource = images, version = "1.11.2" });

            

            pa = new PageInfo<LvData>(imagelist,3);
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoHome);
            
        }
        private void polygon_Click(object sender, RoutedEventArgs e)
        {
            LvData lv = Listview.SelectedItem as LvData;
            if (lv != null)
            {
                Console.WriteLine(lv.version.ToString());
                Listview.Items.RemoveAt(1);
            }
            else
            {
                MessageBox.Show("0");
            }
               
        }

        private void Listview_MouseWheel(object sender, MouseWheelEventArgs e)
        {
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoNext);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoHome);
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoPrePrevious);
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoNext);
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            Listview.ItemsSource = pa.GetPageData(JumpOperation.GoEnd);
        }

        private ChildType FindVisualChild<ChildType>(DependencyObject obj) where ChildType : DependencyObject
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is ChildType)
                {
                    return child as ChildType;
                }
                else
                {
                    ChildType childOfChildren = FindVisualChild<ChildType>(child);
                    if (childOfChildren != null)
                    {
                        return childOfChildren;
                    }
                }
            }
            return null;

        }
    }
}
