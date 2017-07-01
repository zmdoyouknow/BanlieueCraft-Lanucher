using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

namespace BanlieueCraft_Lanucher
{
    public class UserViewModel: BaseNotifyPropertyChanged
    {
        public RelayCommand Way { get; private set; }
        public RelayCommand Java { get; private set; }
        public RelayCommand Memory { get; private set; }
        public RelayCommand Size { get; private set; }
        public UserViewModel()
        {
            this.Way = new RelayCommand(this.ShowWay);
            this.Java = new RelayCommand(this.ShowJava);
            this.Memory = new RelayCommand(this.ShowMemory);
            this.Size = new RelayCommand(this.ShowSize);
        }
        public void ShowWay()
        {
            GameWay gamepath = new GameWay();
            gamepath.ShowDialog();
        }
        public void ShowJava()
        {
            GameJava gamejava = new GameJava();
            gamejava.ShowDialog();
        }
        public void ShowMemory()
        {
            GameMemory gamememory = new GameMemory();
            gamememory.ShowDialog();
        }
        public void ShowSize()
        {
            GameSize gamesize = new GameSize();
            gamesize.ShowDialog();            
        }
    }
}
