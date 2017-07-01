using System;
using System.Collections;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// Version.xaml 的交互逻辑
    /// </summary>
    public partial class GameVersion
    {
        System.Windows.Media.Animation.Storyboard sb = null;
        public GameVersion()
        {
            InitializeComponent();
            sb = (Storyboard)grid.Resources["spread"];
            Game game = new Game();
            sb.Completed += (s, e) =>
            {
                sb = (Storyboard)grid.Resources["shrink"];
                sb.Completed += (sender, Event) => NavigationService?.Navigate(game);;
            };
            sb?.Begin();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var arr = new List<Verdata>();       
            var dirPath = @"I:\.minecraft\versions";
            var theFolder = new DirectoryInfo(dirPath);
            var dfolder = theFolder.GetDirectories();
            foreach (var dname in dfolder)
            {
                string version;

                if (dname.Name.Contains("-"))
                {
                    string op = "OptiFine";
                    string fog = "Forge";
                    string lit = "LiteLoader";
                    if (dname.Name.ToLower().IndexOf(op.ToLower(), StringComparison.Ordinal) > -1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        version = sa[0] + "-OF版";
                    }
                    else if (dname.Name.ToLower().IndexOf(fog.ToLower(), StringComparison.Ordinal) > -1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        if (dname.Name.ToLower().IndexOf(lit.ToLower(), StringComparison.Ordinal) > -1)
                        {
                            version = sa[0] + "-LF版";
                        }
                        else
                        {
                            version = sa[0] + "-FG版";
                        }
                    }
                    else if (dname.Name.ToLower().IndexOf(lit.ToLower(), StringComparison.Ordinal) > -1)
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        if (dname.Name.ToLower().IndexOf(fog.ToLower(), StringComparison.Ordinal) > -1)
                        {
                            version = sa[0] + "-LF版";
                        }
                        else
                        {
                            version = sa[0] + "-LL版";
                        }
                    }
                    else
                    {
                        var sa = Regex.Split(dname.Name, "-", RegexOptions.IgnoreCase);
                        version = sa[0];
                    }
                }
                else
                {
                    version = dname.Name;
                }
                arr.Add(new Verdata() { Version = version ,Ver = dname.Name});               
            }
            Listview.ItemsSource = arr;

        }
        private void button_Click(object sender, RoutedEventArgs e)
        {            
            Button bt = sender as Button;
            Game page = new Game();
            string file = Environment.CurrentDirectory;//读取路径
            string filepatch = file + @"\" + "config.ini"; //配置文件
            IniFile ini = new IniFile(filepatch);
            try
            {
                if (bt != null)
                {
                    page.Verlist = bt.Content.ToString();
                    page.Verlist = bt.Tag.ToString();
                    ini.IniWriteValue("Default", "SelectVer", bt.Content.ToString());
                    ini.IniWriteValue("Default", "SelectRealVer", bt.Tag.ToString());
                }
                //NavigationService?.Navigate(page);
                sb?.Begin();
            }
            catch
            {
                page.Verlist = ini.IniReadValue("Default", "SelectVer");
                page.Realver = ini.IniReadValue("Default", "SelectRealVer");
                //NavigationService?.Navigate(page);
                sb?.Begin();
            }           
        }

        private class Verdata
        {
            public string Version { get; set; }
            public string Ver { get; set; }
        }

        private void button_Click_1(object sender, RoutedEventArgs e)
        {
            sb?.Begin();
        }
    }
}
