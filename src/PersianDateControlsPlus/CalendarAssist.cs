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

        public static readonly DependencyProperty IsSelectedDateProperty =
            DependencyProperty.RegisterAttached("IsSelectedDate", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(false));
        #endregion

        #region AttachedProperty : IsOutOfSpecifiedRange
        public static bool GetIsOutOfSpecifiedRange(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOutOfSpecifiedRangeProperty);
        }

        public static void SetIsOutOfSpecifiedRange(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOutOfSpecifiedRangeProperty, value);
        }

        public static readonly DependencyProperty IsOutOfSpecifiedRangeProperty =
            DependencyProperty.RegisterAttached("IsOutOfSpecifiedRange", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(false));
        #endregion

        #region AttachedProperty : IsOutOfCurrentMonth
        public static bool GetIsOutOfCurrentMonth(DependencyObject obj)
        {
            return (bool)obj.GetValue(IsOutOfCurrentMonthProperty);
        }

        public static void SetIsOutOfCurrentMonth(DependencyObject obj, bool value)
        {
            obj.SetValue(IsOutOfCurrentMonthProperty, value);
        }

        // Using a DependencyProperty as the backing store for IsOutOfCurrentMonth.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOutOfCurrentMonthProperty =
            DependencyProperty.RegisterAttached("IsOutOfCurrentMonth", typeof(bool), typeof(CalendarAssist), new PropertyMetadata(false));
        #endregion
    }
}
