using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using BanlieueCraft_Lanucher.Control;

namespace BanlieueCraft_Lanucher
{
    public class UserViewModel: BaseNotifyPropertyChanged
    {
        public RelayCommand Way { get; private set; }
        public RelayCommand Java { get; private set; }
        public RelayCommand Memory { get; private set; }
        public RelayCommand Size { get; private set; }
        public RelayCommand Vers { get; private set; }
        public UserViewModel()
        {
            Way = new RelayCommand(ShowWay);
            Java = new RelayCommand(ShowJava);
            Memory = new RelayCommand(ShowMemory);
            Size = new RelayCommand(ShowSize);
            Vers = new RelayCommand(ShowVers);
        }
        public void ShowWay()
        {
            var gamepath = new Page.ViewPage.GameWay();
            gamepath.ShowDialog();
        }
        public void ShowJava()
        {
            var gamejava = new Page.ViewPage.GameJava();
            gamejava.ShowDialog();
        }
        public void ShowMemory()
        {
            var gamememory = new Page.ViewPage.GameMemory();
            gamememory.ShowDialog();
        }
        public void ShowSize()
        {
            var gamesize = new Page.ViewPage.GameSize();
            gamesize.ShowDialog();            
        }
        public void ShowVers()
        {
            var gameversion = new Page.Game();
            gameversion.Frame.Navigate(new Uri("/Page/GameVersion.xaml", UriKind.Relative));
        }
    }
}
