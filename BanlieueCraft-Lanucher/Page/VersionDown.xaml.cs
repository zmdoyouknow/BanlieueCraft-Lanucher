using System;
using System.Collections.Generic;
using System.Data;
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
using MySql.Data;
using System.Configuration;
using MySql.Data.MySqlClient;
using LitJson;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// PlayerData.xaml 的交互逻辑
    /// </summary>
    public partial class VersionDown
    {
        //List<listdata> list = new List<listdata>();
        //string connstr = ConfigurationManager.AppSettings["ConnectionStrings"];
        //string sql = ConfigurationManager.AppSettings["SelectStrings"];
        //public class listdata
        //{
        //    public int id { get; set; }
        //    public string name { get; set; }
        //    public string ip { get; set; }
        //    public string lastlogin { get; set; }
        //    public int x { get; set; }
        //    public int y { get; set; }
        //    public int z { get; set; }
        //    public string world { get; set; }
        //    public string islogin { get; set; }
        //}

        public VersionDown()
        {
            InitializeComponent();
        }

        private readonly List<Listdata> _list = new List<Listdata>();
        List<Forgelistdata> _forgelist = new List<Forgelistdata>();
        private IEnumerable<Listdata> _query = null;
        private string _strjson;
        public class Listdata
        {
            public string Version { get; set; }
            public string Time { get; set; }
            public string Name { get; set; }
            public string Url { get; set; }
        }
        public class Forgelistdata
        {
            public string Forgever { get; set; }
            public string Forgetime { get; set; }
            public string ForgeName { get; set; }
            public string Forgeurl { get; set; }
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {            

        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {
            WaitingBox.Show(() =>
            {
                string file = Environment.CurrentDirectory;//读取路径
                string filepatch = file + @"\" + "config.ini"; //配置文件
                IniFile ini = new IniFile(filepatch);
                if (bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && !bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
                {
                    _strjson = GetJson.GetUrlContent("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                }
                if (!bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
                {
                    _strjson = GetJson.GetUrlContent("http://bmclapi2.bangbang93.com/mc/game/version_manifest.json");
                }                
                //string strjson = File.ReadAllText(@"F:\Chrome\version_manifest.json");
                JsonData verdata = JsonMapper.ToObject(_strjson);
                JsonData verjson = verdata["versions"];
                for (int i = 0; i < verjson.Count; i++)
                {
                    string[] tm = verjson[i]["time"].ToString().Split('T', '+');
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
                    else if (verjson[i]["type"].ToString().IndexOf("old", StringComparison.Ordinal) > -1)
                    {
                        vs = "远古版";
                    }
                    else
                    {
                        vs = verjson[i]["type"].ToString();
                    }
                    var data = new Listdata() { Version = verjson[i]["id"].ToString(), Time = tmi, Name = vs, Url = verjson[i]["url"].ToString() };
                    _list.Add(data);
                }
                _query = from items in _list orderby items.Name descending select items;
            }, "正在获取官方版本信息，请稍后...");

            listView.ItemsSource = _query;
            _strjson = null;
        }
        private void textblock_Loaded(object sender, RoutedEventArgs e)
        {
            TextBlock t = sender as TextBlock;
            if (t != null && t.Text == "远古版")
            {
                t.Foreground = Brushes.Red;
            }
            if (t != null && t.Text == "正式版")
            {
                t.Foreground = Brushes.Green;
            }
            if (t != null && t.Text == "快照版")
            {
                t.Foreground = Brushes.IndianRed;
            }
        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            string file = Environment.CurrentDirectory;//读取路径
            string filepatch = file + @"\" + "config.ini"; //配置文件
            IniFile ini = new IniFile(filepatch);
            if (bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && !bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
            {
                _strjson = GetJson.GetUrlContent("https://launchermeta.mojang.com/mc/game/version_manifest.json");
            }
            if (!bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
            {
                _strjson = "http://bmclapi2.bangbang93.com/forge/minecraft/" + ini.IniReadValue("Default", "SelectRealVer");
            }
            
            
        }

        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxX.Warning("WAAAAAAAAAAAAA");
        }
    }
}
