using System;
using System.ComponentModel;
using System.Linq.Expressions;

namespace BanlieueCraft_Lanucher.Control
{
    /// <summary>
    /// 实现了属性更改通知的基类
    /// </summary>
    public class BaseNotifyPropertyChanged : INotifyPropertyChanged
    {
        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyName"></param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// 属性值变化时发生
        /// </summary>
        /// <param name="propertyExpression"></param>
        protected virtual void OnPropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var memberExpression = propertyExpression.Body as MemberExpression;
            if (memberExpression == null) return;
            var propertyName = memberExpression.Member.Name;
            OnPropertyChanged(propertyName);
        }

        public virtual event PropertyChangedEventHandler PropertyChanged;
    }
}