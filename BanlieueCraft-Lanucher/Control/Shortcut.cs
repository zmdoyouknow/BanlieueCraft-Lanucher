using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWshRuntimeLibrary;
using System.Reflection;
using System.Diagnostics;

namespace BanlieueCraft_Lanucher
{
    class Shortcut
    {
        /// <summary>
        /// 创建桌面快捷方式
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="description">描述信息</param>
        public static bool CreateShortCut( string name, string description)
        {
            try
            {
                string deskTop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string dirPath = Environment.CurrentDirectory;
                string exePath = Process.GetCurrentProcess().MainModule.FileName;
                //System.Diagnostics.FileVersionInfo exeInfo = System.Diagnostics.FileVersionInfo.GetVersionInfo(exePath);
                if (System.IO.File.Exists(string.Format(@"{0}\{1}.lnk", deskTop, name)))
                {
                    //  System.IO.File.Delete(string.Format(@"{0}\快捷键名称.lnk",deskTop));//删除原来的桌面快捷键方式
                    return false;
                }
                WshShell shell = new WshShell();
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + "\\" + name + ".lnk");
                shortcut.TargetPath = @exePath;         //目标文件
                shortcut.WorkingDirectory = dirPath;    //目标文件夹
                shortcut.WindowStyle = 1;               //目标应用程序的窗口状态分为普通、最大化、最小化【1,3,7】
                shortcut.Description = description;   //描述
                shortcut.IconLocation = string.Format(@"{0}\64.ico", dirPath);  //快捷方式图标
                shortcut.Arguments = "";
                shortcut.Hotkey = "";              // 快捷键
                shortcut.Save();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
