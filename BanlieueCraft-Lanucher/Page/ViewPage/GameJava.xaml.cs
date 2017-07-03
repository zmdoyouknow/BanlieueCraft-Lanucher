using System.Windows;
using System.Windows.Input;

namespace BanlieueCraft_Lanucher.Page.ViewPage
{
    /// <summary>
    /// Java.xaml 的交互逻辑
    /// </summary>
    public partial class GameJava : Window
    {
        public GameJava()
        {
            InitializeComponent();
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
