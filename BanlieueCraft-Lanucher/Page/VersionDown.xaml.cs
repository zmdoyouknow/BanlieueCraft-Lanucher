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
using System.IO;
using System.Threading;
using System.Windows.Forms;

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
        private readonly List<Listdata> _list1 = new List<Listdata>();
        private readonly List<Listdata> _list2 = new List<Listdata>();
        List<Forgelistdata> _forgelist = new List<Forgelistdata>();
        private IEnumerable<Listdata> _query;
        private IEnumerable<Listdata> _query1;
        private IEnumerable<Listdata> _query2;
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
            WaitingBox.Show(() =>
            {
                var file = Environment.CurrentDirectory;//读取路径
                var filepatch = file + @"\" + "config.ini"; //配置文件
                var ini = new IniFile(filepatch);
                if (bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && !bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
                {
                    _strjson = GetJson.GetUrlContent("https://launchermeta.mojang.com/mc/game/version_manifest.json");
                }
                if (!bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
                {
                    _strjson = GetJson.GetUrlContent("http://bmclapi2.bangbang93.com/mc/game/version_manifest.json");
                }
                //string strjson = File.ReadAllText(@"F:\Chrome\version_manifest.json");
                var verdata = JsonMapper.ToObject(_strjson);
                var verjson = verdata["versions"];
                //var ver = verdata["versions"]["id"].ToString();
                for (int i = 0; i < verjson.Count; i++)
                {
                    string[] tm = verjson[i]["time"].ToString().Split('T', '+');
                    string tmi = tm[0] + "  ||  " + tm[1];
                    //var rela = new List<string>();
                    if (!Directory.Exists($"{Environment.CurrentDirectory}/.minecraft/versions/{verjson[i]["id"]}/"))
                    {
                        if (verjson[i]["type"].ToString() == "release")
                        {
                            var data = new Listdata { Version = verjson[i]["id"].ToString(), Time = tmi, Name = "正式版", Url = verjson[i]["id"] + "|" + verjson[i]["url"] };
                            _list.Add(data);
                        }
                        if (verjson[i]["type"].ToString() == "snapshot")
                        {
                            var data = new Listdata { Version = verjson[i]["id"].ToString(), Time = tmi, Name = "快照版", Url = verjson[i]["id"] + "|" + verjson[i]["url"] };
                            _list1.Add(data);
                        }
                        if (verjson[i]["type"].ToString().IndexOf("old", StringComparison.Ordinal) > -1)
                        {
                            var data = new Listdata { Version = verjson[i]["id"].ToString(), Time = tmi, Name = "远古版", Url = verjson[i]["id"] + "|" + verjson[i]["url"] };
                            _list2.Add(data);
                        }
                    }

                }
                _query = from items in _list orderby items.Name descending select items;
                _query1 = from items in _list1 orderby items.Name descending select items;
                _query2 = from items in _list2 orderby items.Name descending select items;
            }, "正在获取官方版本信息，请稍后...");

            ListView.ItemsSource = _query;
            ListView001.ItemsSource = _query1;
            ListView002.ItemsSource = _query2;
            _strjson = null;
        }

        private void Grid_Loaded(object sender, RoutedEventArgs e)
        {

        }
        private void textblock_Loaded(object sender, RoutedEventArgs e)
        {
            var t = sender as TextBlock;
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

        /// <summary>
        /// 查找子控件
        /// </summary>
        /// <typeparam name="T">子控件的类型</typeparam>
        /// <param name="obj">要找的是obj的子控件</param>
        /// <param name="name">想找的子控件的Name属性</param>
        /// <returns>目标子控件</returns>
        public static T GetChildObject<T>(DependencyObject obj, string name) where T : FrameworkElement
        {
            DependencyObject child = null;
            T grandChild = null;

            for (int i = 0; i <= VisualTreeHelper.GetChildrenCount(obj) - 1; i++)
            {
                child = VisualTreeHelper.GetChild(obj, i);

                if (child is T && (((T)child).Name == name | string.IsNullOrEmpty(name)))
                {
                    return (T)child;
                }
                else
                {
                    // 在下一级中没有找到指定名字的子控件，就再往下一级找
                    grandChild = GetChildObject<T>(child, name);
                    if (grandChild != null)
                        return grandChild;
                }
            }

            return null;

        }

        private void Grid_Loaded_1(object sender, RoutedEventArgs e)
        {
            //string file = Environment.CurrentDirectory;//读取路径
            //string filepatch = file + @"\" + "config.ini"; //配置文件
            //IniFile ini = new IniFile(filepatch);
            //if (bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && !bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
            //{
            //    _strjson = GetJson.GetUrlContent("https://launchermeta.mojang.com/mc/game/version_manifest.json");
            //}
            //if (!bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))
            //{
            //    _strjson = "http://bmclapi2.bangbang93.com/forge/minecraft/" + ini.IniReadValue("Default", "SelectRealVer");
            //}
            
            
        }
        private void FButton_Click(object sender, RoutedEventArgs e)
        {
            //var file = @"I:\";
            var bt = sender as Button;
            if (bt == null) return;
            var url = bt.Tag.ToString().Split('|');
            //Console.WriteLine(bt.Tag.ToString());
            var downroad = new DownRoad
            {
                Url = url[1],
                Ver = url[0],
                Label1 = {Content = "正在下载json文件..."}
            };
            downroad.ShowDialog();
        }

    }
}
