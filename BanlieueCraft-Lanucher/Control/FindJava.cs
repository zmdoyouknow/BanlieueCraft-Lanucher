using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BanlieueCraft_Lanucher
{
    class FindJava
    {
        public static ArrayList JavaList()
        {
            ArrayList javalist = new ArrayList();
            bool type;
            type = Environment.Is64BitOperatingSystem; //判断系统位数
            if (type)         //64位系统
            {
                try  //尝试读取
                {    //尝试优先读取64位java
                    var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
                    localKey = localKey.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                    if (localKey != null)
                        foreach (var keyname in localKey.GetSubKeyNames())
                        {
                            var ver = localKey.OpenSubKey(keyname);
                            if (ver != null)
                            {
                                var vers = ver.GetValue("JavaHome").ToString();
                                var javapatch = vers + @"\" + "bin" + @"\" + "javaw.exe";
                                javalist.Add(javapatch);
                            }
                        }
                }
                catch   //64位读取失败再尝试读取32位
                {
                    var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                    localKey = localKey.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                    if (localKey != null)
                        foreach (var keyname in localKey.GetSubKeyNames())
                        {
                            var ver = localKey.OpenSubKey(keyname);
                            if (ver != null)
                            {
                                var vers = ver.GetValue("JavaHome").ToString();
                                var javapatch = vers + @"\" + "bin" + @"\" + "javaw.exe";
                                javalist.Add(javapatch);
                            }
                        }
                }
            }
            else                      //32位系统
            {
                try  //尝试读取32位java
                {
                    var localKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry32);
                    localKey = localKey.OpenSubKey(@"SOFTWARE\JavaSoft\Java Runtime Environment");
                    if (localKey != null)
                        foreach (var keyname in localKey.GetSubKeyNames())
                        {
                            var ver = localKey.OpenSubKey(keyname);
                            if (ver != null)
                            {
                                var vers = ver.GetValue("JavaHome").ToString();
                                var javapatch = vers + @"\" + "bin" + @"\" + "javaw.exe";
                                javalist.Add(javapatch);
                            }
                        }
                }
                catch //读取失败，提示
                {
                    var javapatch = "读取失败";
                    javalist.Add(javapatch);
                }
            }
            return javalist;
        }
    }
 }
