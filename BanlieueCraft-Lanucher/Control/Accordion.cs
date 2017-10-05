using System.Windows;
using System.Windows.Controls;

namespace BanlieueCraft_Lanucher.Control
{
    public class Accordion : ListBox
    {
        #region private fields

        #endregion

        #region DependencyProperty

        #endregion

        #region Constructors

        static Accordion()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Accordion), new FrameworkPropertyMetadata(typeof(Accordion)));
        }

        #endregion

        #region Override

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
        }

        #endregion

        #region private function

        #endregion

        #region Event Implement Function

        #endregion
    }
}
