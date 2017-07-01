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
using System.Windows.Navigation;
using System.Windows.Shapes;
using LitJson;
using System.IO;
using System.Reflection;
using System.Data;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// VerDown.xaml 的交互逻辑
    /// </summary>
    public partial class VerDown
    {
        public VerDown()
        {
            InitializeComponent();
        }
        List<listdata> list = new List<listdata>();
        IEnumerable<listdata> query = null;
        string _VerUrl;
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            WaitingBox.Show(() =>
            {
                string strjson = GetJson.GetUrlContent("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                //string strjson = File.ReadAllText(@"F:\Chrome\version_manifest.json");
                JsonData verdata = JsonMapper.ToObject(strjson);
                JsonData verjson = verdata["versions"];
                for (int i = 0; i < verjson.Count; i++)
                {
                    string[] tm = verjson[i]["time"].ToString().Split(new char[2] { 'T', '+' });
                    string tmi = tm[0] + "  ||  " + tm[1];
                    string vs;
                    if (verjson[i]["type"].ToString() == "snapshot")
                    {
                        vs = "快照版";
                    }
                    else if (verjson[i]["type"].ToString() == "release")
                    {
                        vs = "正式版";
                        //text.Foreground = Brushes.Green;
                    }
                    else if (verjson[i]["type"].ToString().IndexOf("old") > -1)
                    {
                        vs = "远古版";
                    }
                    else
                    {
                        vs = verjson[i]["type"].ToString();
                    }
                    var data = new listdata() { version = verjson[i]["id"].ToString(), time = tmi, Name = vs, url = verjson[i]["url"].ToString() };
                    list.Add(data);
                }
                query = from items in list orderby items.Name descending select items;
            }, "正在获取官方版本信息，请稍后...");
            
            listView.ItemsSource = query ;
        }
        
        public class listdata
        {
            public string version { get; set; }
            public string time { get; set; }
            public string Name { get; set; }
            public string url { get; set; }
        }
        private void textblock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock t = sender as TextBlock;
            if (t.Text == "远古版")
            {
                t.Foreground = Brushes.Red;
            }
            if (t.Text == "正式版")
            {
                t.Foreground = Brushes.Green;
            }
            if (t.Text == "快照版")
            {
                t.Foreground = Brushes.IndianRed;
            }
        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            listdata ld = listView.SelectedItem as listdata;
            if (ld != null && ld is listdata)
            {
                _VerUrl = ld.url;
            }
            Console.WriteLine(_VerUrl);
        }
    }
}
