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
            this.datePicker.DisplayDateStart = this.calendar.DisplayDateStart = (new PersianDateControlsPlus.PersianDate.PersianDate(1, 1, 1)).ToDateTime();
            this.datePicker.DisplayDateEnd = this.calendar.DisplayDateEnd = DateTime.MaxValue;

            this.persianDatePicker.DisplayDateStart = this.persianCalendar.DisplayDateStart = new PersianDateControlsPlus.PersianDate.PersianDate(1402, 11, 7);
            this.persianDatePicker.DisplayDateEnd = this.persianCalendar.DisplayDateEnd = new PersianDateControlsPlus.PersianDate.PersianDate(DateTime.MaxValue);
        }
    }
}
