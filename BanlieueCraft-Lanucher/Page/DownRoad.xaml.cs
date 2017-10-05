using System;
using System.ComponentModel;
using System.IO;
using System.Net;
using System.Security.Policy;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using LitJson;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;

namespace BanlieueCraft_Lanucher.Page
{
    /// <summary>
    /// DownRoad.xaml 的交互逻辑
    /// </summary>
    public partial class DownRoad : Window
    {
        public string Url;
        public string Ver;
        public int TotalFile;
        public int LoadFile;
        public int Tempint1;
        public int Tempint2;
        public WebClient Wc = new WebClient();
        public ArrayList Pathlist = new ArrayList();

        public DownRoad()
        {
            InitializeComponent();
            Wc.DownloadProgressChanged += wc_DownloadProgressChanged;
        }


        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //Console.WriteLine(Path.GetFileName(Url));
            if (!Directory.Exists($"{Environment.CurrentDirectory}/.minecraft/versions/{Ver}/")) 
            {
                Directory.CreateDirectory($"{Environment.CurrentDirectory}/.minecraft/versions/{Ver}/"); //版本文件夹不存在新建文件夹   
            }
            var file = Environment.CurrentDirectory;//读取路径
            var filepatch = file + @"\" + "config.ini"; //配置文件
            var ini = new IniFile(filepatch);
            if (bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) &&
                !bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))                              //官方
            {
                var task = Wc.DownloadFileTaskAsync(Url,
                    string.Format("{0}/.minecraft/versions/{1}/{1}.json", Environment.CurrentDirectory, Ver));
                await task;
                Label1.Content = Path.GetFileName(Url) + "下载完成! 开始下载本体文件...";
                var cljson =
                    GetJson.GetUrlContent(string.Format("{0}/.minecraft/versions/{1}/{1}.json",
                        Environment.CurrentDirectory, Ver));
                var clientdata = JsonMapper.ToObject(cljson);
                var clienturl = clientdata["downloads"]["client"]["url"].ToString();
                //Console.WriteLine(clint);
                var clienttask = Wc.DownloadFileTaskAsync(clienturl,
                    string.Format("{0}/.minecraft/versions/{1}/{1}.jar", Environment.CurrentDirectory, Ver));
                await clienttask;
                Label1.Content = Ver + ".jar本体文件下载完成";
            }
            if (!bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Mojang")) && 
                bool.Parse(ini.IniReadValue("Default", "OnDownRoad_Zhcn")))                                  //国内
            {
                //wc.Headers.Add("user-agent", ".NET Framework Test Client");
                TotalFile++;
                var task = Wc.DownloadFileTaskAsync($"https://bmclapi2.bangbang93.com/version/{Ver}/json",
                    string.Format("{0}/.minecraft/versions/{1}/{1}.json", Environment.CurrentDirectory, Ver));
                await task;
                TotalFile++;
                Label1.Content = Path.GetFileName(Url) + "下载完成! 开始下载本体文件...";
                //Console.WriteLine(clint);
                var clienttask = Wc.DownloadFileTaskAsync($"https://bmclapi2.bangbang93.com/version/{Ver}/client",
                    string.Format("{0}/.minecraft/versions/{1}/{1}.jar", Environment.CurrentDirectory, Ver));
                await clienttask;
                Label1.Content = Ver + ".jar本体文件下载完成! 开始下载缺失文件...";
                var cljson =GetJson.GetUrlContent(string.Format("{0}/.minecraft/versions/{1}/{1}.json",Environment.CurrentDirectory, Ver));
                var clientdata = JsonMapper.ToObject(cljson);
                //Console.WriteLine(clientdata["libraries"].Count);
                var libdata = clientdata["libraries"];
                for (var i = 0; i < libdata.Count; i++)
                {
                    //Console.WriteLine(libdata[i]["downloads"].ToString());
                    var downloads = libdata[i]["downloads"];
                    if (downloads.Keys.Contains("artifact"))
                    {
                        var artifact = downloads["artifact"];
                        var path = artifact["path"].ToString();
                        //Console.WriteLine(path);
                        Pathlist.Add(path);
                        Tempint1 = artifact.Count;
                    }
                    if (downloads.Keys.Contains("classifiers"))
                    {
                        var classifiers = downloads["classifiers"];
                        Tempint2 = classifiers.Count;
                        if (classifiers.Keys.Contains("natives-windows"))
                        {
                            var path = classifiers["natives-windows"]["path"].ToString();
                            Console.WriteLine(path);
                            Pathlist.Add(path);
                        }
                    }                    
                    //var jarname = libdata[i]["name"].ToString();
                    //var splitname = jarname.Split(':');
                    //var pathdir = splitname[0].Replace(".", "\\") + "\\" + splitname[1] + "\\" + splitname[2];
                    //var pathjar = splitname[0].Replace(".", "\\") + "\\" + splitname[1] + "\\" + splitname[2] + "\\" + splitname[1] + "-" + splitname[2] + ".jar";
                    //if (!Directory.Exists(Environment.CurrentDirectory + @"\.minecraft\libraries\" + pathdir))
                    //{
                    //    //路径内文件夹不存在    创建
                    //   // Directory.CreateDirectory(Environment.CurrentDirectory + @"\.minecraft\libraries\" + pathdir);
                    //}
                    //if (!File.Exists(Environment.CurrentDirectory + @"\.minecraft\libraries\" + pathjar))
                    //{
                    //    //文件不存在   下载                        
                    //    //var jarurl = "https://bmclapi2.bangbang93.com/libraries/" + pathjar.Replace(@"\", "/");
                    //    //var path = Environment.CurrentDirectory + @"\.minecraft\libraries\" + pathjar;
                    //    //if (CheckUrl(jarurl))
                    //    //{
                    //    //    //Console.WriteLine(jarurl);
                    //    //    var task2 = wc.DownloadFileTaskAsync(jarurl, path);
                    //    //    await task2;
                    //    //    TotalFile++;
                    //    //}

                    //}
                }
                TotalFile += Tempint1 + Tempint2;
                foreach (var path in Pathlist)
                {                    
                    //Console.WriteLine(path);
                    //TotalFile++;
                    var dir = path.ToString().Substring(0,path.ToString().LastIndexOf("/", StringComparison.Ordinal));
                    var pathurl = "https://bmclapi2.bangbang93.com/libraries/" + path;
                    var pathdir = Environment.CurrentDirectory + @"\.minecraft\libraries\" + path;
                    if (!Directory.Exists(Environment.CurrentDirectory + @"\.minecraft\libraries\" + dir))
                    {
                        //路径内文件夹不存在    创建
                        Directory.CreateDirectory(Environment.CurrentDirectory + @"\.minecraft\libraries\" + dir);
                    }
                    if (!File.Exists(Environment.CurrentDirectory + @"\.minecraft\libraries\" + path))
                    {
                        if (CheckUrl(pathurl))
                        {
                            var task2 = Wc.DownloadFileTaskAsync(pathurl, pathdir);
                            await task2;
                        }
                    }
                }
                Label1.Content = "下载完成...";
                Button.IsEnabled = true;
            }
        }

        public bool CheckUrl(string url)
        {
            try
            {
                var req = (HttpWebRequest)WebRequest.Create(url);
                req.Timeout = 100;
                var resp = (HttpWebResponse)req.GetResponse();
                if (resp.StatusCode == HttpStatusCode.OK)
                {
                    resp.Close();
                    return true;
                }
            }
            catch (WebException)
            {
                return false;
            }

            return false;

        }

        private void wc_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {            
            Dispatcher.Invoke(new MethodInvoker(delegate {
                Pro2.Value = e.ProgressPercentage;
                
            }));
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Close();
            var vd = new MainWindow();
            vd.PageContext.Refresh();
        }
    }
}
