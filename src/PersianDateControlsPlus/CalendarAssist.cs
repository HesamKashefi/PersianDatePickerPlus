using System.Windows;

namespace PersianDateControlsPlus
{
    public static class CalendarAssist
    {
        #region AttachedProperty : IsCurrentDay
        public static bool GetIsCurrentDay(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsCurrentDayProperty);
        }

        public static void SetIsCurrentDay(DependencyObject obj, bool value)
        {
            obj.SetValue(IsCurrentDayProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsCurrentDay.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsCurrentDayProperty =
            DependencyProperty.RegisterAttached("IsCurrentDay", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(false));
        #endregion

        #region AttachedProperty : IsSelectedDate
        public static bool GetIsSelectedDate(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsSelectedDateProperty);
        }

        public static void SetIsSelectedDate(DependencyObject obj, bool value)
        {
            obj.SetValue(IsSelectedDateProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsSelectedDate.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsSelectedDateProperty =
            DependencyProperty.RegisterAttached("IsSelectedDate", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(false));
        #endregion
    }
}
