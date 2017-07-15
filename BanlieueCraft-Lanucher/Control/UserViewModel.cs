using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Navigation;


namespace BanlieueCraft_Lanucher.Control
{
    public class UserViewModel: BaseNotifyPropertyChanged
    {
        public static RelayCommand Way { get; private set; }
        public static RelayCommand Java { get; private set; }
        public static RelayCommand Memory { get; private set; }
        public static RelayCommand Size { get; private set; }

        public static RelayCommand Jiedian { get; set; }

        //public static readonly CommandBinding VersBinding;
        public UserViewModel()
        {
            Way = new RelayCommand(ShowWay);
            Java = new RelayCommand(ShowJava);
            Memory = new RelayCommand(ShowMemory);
            Size = new RelayCommand(ShowSize);
            Jiedian = new RelayCommand(ShowJD);
        }

        public static void ShowWay()
        {
            var gamepath = new Page.ViewPage.GameWay();
            gamepath.ShowDialog();
        }
        public static void ShowJava()
        {
            var gamejava = new Page.ViewPage.GameJava();
            gamejava.ShowDialog();
        }
        public static void ShowMemory()
        {
            var gamememory = new Page.ViewPage.GameMemory();
            gamememory.ShowDialog();
        }
        public static void ShowSize()
        {
            var gamesize = new Page.ViewPage.GameSize();
            gamesize.ShowDialog();            
        }

        private void ShowJD()
        {
            MessageBox.Show("111");
        }        
    }
}
