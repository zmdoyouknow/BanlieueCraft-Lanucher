using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Controls;
using System.Windows.Forms;

namespace BanlieueCraft_Lanucher
{
    class PageInfo<T>
    {
        public List<T> dataSource;
        int pageSize;
        int pagecount;
        int currentIndex;

        public PageInfo()
        {
            currentIndex = 1;
        }

        public PageInfo(List<T> dataSource, int pageSize)
            : this()
        {
            this.dataSource = dataSource;
            this.pageSize = pageSize;
            this.pagecount = dataSource.Count / pageSize;
            this.pagecount += (dataSource.Count % pageSize) != 0 ? 1 : 0;
        }

        public List<T> GetPageData(JumpOperation jo)
        {
            switch (jo)
            {
                case JumpOperation.GoHome:
                    currentIndex = 1;
                    break;
                case JumpOperation.GoPrePrevious:
                    if (currentIndex > 1)
                    {
                        currentIndex -= 1;
                    }
                    break;
                case JumpOperation.GoNext:
                    if (currentIndex < pagecount)
                    {
                        currentIndex += 1;
                    }
                    break;
                case JumpOperation.GoEnd:
                    currentIndex = pagecount;
                    break;
            }
            List<T> listPageData = new List<T>();
            try
            {
                int pageCountTo = pageSize;
                if (pagecount == currentIndex && dataSource.Count % pageSize > 0)
                {
                    pageCountTo = dataSource.Count % pageSize;
                }
                if (null != dataSource)
                {
                    for (int i = 0; i < pageCountTo; i++)
                    {
                        if ((currentIndex - 1) * pageSize + i < dataSource.Count)
                        {
                            listPageData.Add(dataSource[(currentIndex - 1) * pageSize + i]);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                return listPageData;
            }
            catch
            {
                return listPageData;
            }
        }
    }
    public enum JumpOperation
    {
        GoHome = 0,
        GoPrePrevious = 1,
        GoNext = 2,
        GoEnd = 3
    }
}

