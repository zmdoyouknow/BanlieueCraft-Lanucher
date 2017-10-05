using BanlieueCraft_Lanucher;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace System.Windows
{
    /// <summary>
    /// MessageBoxXxaml.xaml 的交互逻辑
    /// </summary>
    public partial class MessageBoxX : Window
    {
        /// <summary>
        /// 结果，用户点击确定Result=true;
        /// </summary>
        public bool Result { get; private set; }

       

        private static readonly Dictionary<string, Brush> _Brushes = new Dictionary<string, Brush>();

        public MessageBoxX(EnumNotifyType type, string mes)
        {
            InitializeComponent();
            txtMessage.Text = mes;
            
            //type
            btnCancel.Visibility = Visibility.Collapsed;
            switch (type)
            {
                case EnumNotifyType.Error:
                    tishi.Text = "!";
                    break;
                case EnumNotifyType.Warning:
                    tishi.Text = "!";
                    break;
                case EnumNotifyType.Info:
                    tishi.Text = "!";
                    break;
                case EnumNotifyType.Question:
                    tishi.Text = "?";
                    btnCancel.Visibility = Visibility.Visible;
                    break;
            }
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            Result = true;
            Close();
            e.Handled = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            Result = false;
            Close();
            e.Handled = true;
        }

        /********************* public static method **************************/

        /// <summary>
        /// 提示错误消息
        /// </summary>
        public static void Error(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Error, mes, owner);
        }

        /// <summary>
        /// 提示普通消息
        /// </summary>
        public static void Info(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Info, mes, owner);
        }

        /// <summary>
        /// 提示警告消息
        /// </summary>
        public static void Warning(string mes, Window owner = null)
        {
            Show(EnumNotifyType.Warning, mes, owner);
        }

        /// <summary>
        /// 提示询问消息
        /// </summary>
        public static bool Question(string mes, Window owner = null)
        {
            return Show(EnumNotifyType.Question, mes, owner);
        }

        /// <summary>
        /// 显示提示消息框，
        /// owner指定所属父窗体，默认参数值为null，则指定主窗体为父窗体。
        /// </summary>
        private static bool Show(EnumNotifyType type, string mes, Window owner = null)
        {
            var res = true;
            Application.Current.Dispatcher.Invoke(new Action(() =>
            {
                MessageBoxX nb = new MessageBoxX(type, mes) { Title = GetEnumDescription(type) };
                nb.Owner = owner ?? ControlHelper.GetTopWindow();
                nb.ShowDialog();
                res = nb.Result;
            }));
            return res;
        }
        
        public static string GetEnumDescription(Enum enumSubitem)
        {
            string strValue = enumSubitem.ToString();

            FieldInfo fieldinfo = enumSubitem.GetType().GetField(strValue);
            Object[] objs = fieldinfo.GetCustomAttributes(typeof(DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
            {
                return strValue;
            }
            else
            {
                DescriptionAttribute da = (DescriptionAttribute)objs[0];
                return da.Description;
            }

        }

        /// <summary>
        /// 通知消息类型
        /// </summary>
        public enum EnumNotifyType
        {
            [Description("错误")]
            Error,
            [Description("警告")]
            Warning,
            [Description("提示信息")]
            Info,
            [Description("询问信息")]
            Question,
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
