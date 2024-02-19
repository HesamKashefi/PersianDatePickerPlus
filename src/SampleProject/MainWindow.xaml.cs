using System;
using System.Windows;

namespace SampleProject
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            this.datePicker.DisplayDateStart = this.calendar.DisplayDateStart =
                (new PersianDateControlsPlus.PersianDate.PersianDate(1, 1, 1)).ToDateTime();
            this.persianDatePicker.DisplayDateEnd = this.persianCalendar.DisplayDateEnd = new PersianDateControlsPlus.PersianDate.PersianDate(DateTime.MaxValue);
            this.datePicker.DisplayDateEnd = this.calendar.DisplayDateEnd = DateTime.MaxValue;
        }
    }
}
