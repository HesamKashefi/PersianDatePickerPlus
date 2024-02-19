using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PersianDateControlsPlus
{
    [System.ComponentModel.DefaultEvent("SelectedDateChanged")]
    [System.ComponentModel.DefaultProperty("DisplayDate")]
    public partial class PersianCalendar : UserControl
    {
        public PersianCalendar()
        {
            InitializeComponent();

            InitializeMonth();
            initializeYear();
            initializeDecade();

            this.setCalendar();
        }

        //Properties

        public static readonly DependencyProperty DisplayDateProperty;
        /// <summary>
        /// Gets or sets the date that is being displayed in the calendar.
        /// </summary>
        [System.ComponentModel.Category("Calendar")]
        public PersianDate.PersianDate DisplayDate
        {
            get
            {
                return (PersianDate.PersianDate)this.GetValue(DisplayDateProperty);
            }
            set
            {
                this.SetValue(DisplayDateProperty, value);
            }
        }

        public static readonly DependencyProperty DisplayModeProperty;
        [System.ComponentModel.Category("Calendar")]
        public CalendarMode DisplayMode
        {
            get { return (CalendarMode)GetValue(DisplayModeProperty); }
            set { SetValue(DisplayModeProperty, value); }
        }


        /// <summary>
        /// the minimum date that is displayed, and can be selected
        /// </summary>
        [System.ComponentModel.Category("Calendar")]
        public PersianDate.PersianDate DisplayDateStart
        {
            get { return (PersianDate.PersianDate)GetValue(DisplayDateStartProperty); }
            set { SetValue(DisplayDateStartProperty, value); }
        }
        public static readonly DependencyProperty DisplayDateStartProperty;


        /// <summary>
        /// the minimum date that is displayed, and can be selected
        /// </summary>
        [System.ComponentModel.Category("Calendar")]
        public PersianDate.PersianDate DisplayDateEnd
        {
            get { return (PersianDate.PersianDate)GetValue(DisplayDateEndProperty); }
            set { SetValue(DisplayDateEndProperty, value); }
        }

        public static readonly DependencyProperty DisplayDateEndProperty;
        [System.ComponentModel.Category("Calendar")]
        public PersianDate.PersianDate? SelectedDate
        {
            get { return (PersianDate.PersianDate?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }
        public static readonly DependencyProperty SelectedDateProperty;


        [System.ComponentModel.Category("Calendar")]
        public Brush SelectedDateBackground
        {
            get { return (Brush)GetValue(SelectedDateBackgroundProperty); }
            set { SetValue(SelectedDateBackgroundProperty, value); }
        }
        public static readonly DependencyProperty SelectedDateBackgroundProperty =
            DependencyProperty.Register("SelectedDateBackground", typeof(Brush), typeof(PersianCalendar), new UIPropertyMetadata(Brushes.Lavender));


        [System.ComponentModel.Category("Calendar")]
        public Brush TodayBackground
        {
            get { return (Brush)GetValue(TodayBackgroundProperty); }
            set { SetValue(TodayBackgroundProperty, value); }
        }
        public static readonly DependencyProperty TodayBackgroundProperty;


        //properties coercions and changed event handlers
        static void DisplayDateStartChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersianCalendar pc = d as PersianCalendar;
            pc.CoerceValue(DisplayDateEndProperty);
            pc.CoerceValue(SelectedDateProperty);
            pc.CoerceValue(DisplayDateProperty);
            modeChanged(d, new DependencyPropertyChangedEventArgs());
        }
        static void DisplayDateEndChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PersianCalendar pc = d as PersianCalendar;
            pc.CoerceValue(SelectedDateProperty);
            pc.CoerceValue(DisplayDateProperty);
            modeChanged(d, new DependencyPropertyChangedEventArgs());

        }

        static object coerceDisplayDateStart(DependencyObject d, object o)
        {
            PersianCalendar pc = d as PersianCalendar;
            PersianDate.PersianDate value = (PersianDate.PersianDate)o;
            return o;

        }
        static object coerceDisplayDateEnd(DependencyObject d, object o)
        {
            PersianCalendar pc = d as PersianCalendar;
            PersianDate.PersianDate value = (PersianDate.PersianDate)o;
            if (value < pc.DisplayDateStart)
            {
                return pc.DisplayDateStart;
            }
            return o;
        }
        static object coerceDateToBeInRange(DependencyObject d, object o)
        {
            PersianCalendar pc = d as PersianCalendar;
            if (o is null) return null;
            PersianDate.PersianDate value = (PersianDate.PersianDate)o;
            if (value < pc.DisplayDateStart)
            {
                return pc.DisplayDateStart;
            }
            if (value > pc.DisplayDateEnd)
            {
                return pc.DisplayDateEnd;
            }
            return o;
        }

        //events

        public static readonly RoutedEvent SelectedDateChangedEvent;
        [System.ComponentModel.Category("Calendar")]
        public event RoutedEventHandler SelectedDateChanged
        {
            add { AddHandler(SelectedDateChangedEvent, value); }
            remove { RemoveHandler(SelectedDateChangedEvent, value); }
        }


        static void modeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is PersianCalendar pc)
                pc.setCalendar();
        }

        static PersianCalendar()
        {

            PropertyMetadata displayModeMetaData = new PropertyMetadata(modeChanged);
            DisplayModeProperty =
                DependencyProperty.Register("DisplayMode", typeof(CalendarMode), typeof(PersianCalendar), displayModeMetaData);

            PropertyMetadata displayDateMetaData = new PropertyMetadata(PersianDate.PersianDate.Today, modeChanged);
            displayDateMetaData.CoerceValueCallback = coerceDateToBeInRange;
            DisplayDateProperty =
                DependencyProperty.Register("DisplayDate", typeof(PersianDate.PersianDate), typeof(PersianCalendar), displayDateMetaData);


            PropertyMetadata selectedDateMetaData = new PropertyMetadata(PersianDate.PersianDate.Today,
            (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            {
                PersianCalendar pc = d as PersianCalendar;
                if (pc != null)
                {
                    pc.selectedDateCheck((PersianDate.PersianDate?)e.OldValue);
                    pc.RaiseEvent(new RoutedEventArgs(SelectedDateChangedEvent, pc));
                }
            }
            );
            selectedDateMetaData.CoerceValueCallback = coerceDateToBeInRange;
            SelectedDateProperty =
                DependencyProperty.Register("SelectedDate", typeof(PersianDate.PersianDate?), typeof(PersianCalendar), selectedDateMetaData);

            PropertyMetadata displayDateStartMetaData = new PropertyMetadata
            {
                DefaultValue = new PersianDate.PersianDate(),
                CoerceValueCallback = new CoerceValueCallback(coerceDisplayDateStart),
                PropertyChangedCallback = new PropertyChangedCallback(DisplayDateStartChanged),
            };

            DisplayDateStartProperty =
                DependencyProperty.Register("DisplayDateStart", typeof(PersianDate.PersianDate), typeof(PersianCalendar), displayDateStartMetaData);

            PropertyMetadata displayDateEndMetaData = new PropertyMetadata
            {
                DefaultValue = new PersianDate.PersianDate(10000, 1, 1),
                CoerceValueCallback = new CoerceValueCallback(coerceDisplayDateEnd),
                PropertyChangedCallback = new PropertyChangedCallback(DisplayDateEndChanged),
            };

            DisplayDateEndProperty =
                DependencyProperty.Register("DisplayDateEnd", typeof(PersianDate.PersianDate), typeof(PersianCalendar), displayDateEndMetaData);


            PropertyMetadata todayBackgroundMetaData = new PropertyMetadata(Brushes.AliceBlue,
            (DependencyObject d, DependencyPropertyChangedEventArgs e) =>
            {
                PersianCalendar pc = d as PersianCalendar;
                if (pc != null)
                {
                    pc.todayCheck();
                }
            }
            );
            TodayBackgroundProperty =
                DependencyProperty.Register("TodayBackground", typeof(Brush), typeof(PersianCalendar), todayBackgroundMetaData);

            SelectedDateChangedEvent = EventManager.RegisterRoutedEvent("SelectedDateChanged",
                RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PersianCalendar));

        }
        private Button newControl()
        {
            var element = new Button
            {
                HorizontalAlignment = HorizontalAlignment.Stretch,
                VerticalAlignment = VerticalAlignment.Stretch,
                HorizontalContentAlignment = HorizontalAlignment.Center,
                VerticalContentAlignment = VerticalAlignment.Center,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Thickness(0),
                FontSize = 10,
                Style = (Style)this.FindResource("InsideButtonsStyle"),
                Background = Brushes.Transparent,
                //ContentTemplate=(DataTemplate)this.FindResource("InsideButtonContentTemplate"),
            };
            return element;
        }

        #region Month
        public readonly Button[,] MonthModeButtons = new Button[6, 7];
        private readonly static string[] _daysOfWeek = new string[] { "ش", "١ش", "٢ش", "٣ش", "٤ش", "٥ش", "ج" };
        void InitializeMonth()
        {
            for (int j = 1; j <= 7; j++)
            {
                var element = new Label
                {
                    HorizontalAlignment = HorizontalAlignment.Stretch,
                    VerticalAlignment = VerticalAlignment.Stretch,
                    HorizontalContentAlignment = HorizontalAlignment.Center,
                    VerticalContentAlignment = VerticalAlignment.Top,
                    Padding = new Thickness(0),
                    FontWeight = FontWeights.SemiBold,
                    Style = (Style)this.FindResource("InsideLabelStyle"),
                    Content = _daysOfWeek[j - 1],
                };

                this.monthUniformGrid.Children.Add(element);
            }
            int tabIndex = 10;
            for (int i = 2; i <= 7; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    var element = newControl();
                    element.TabIndex = tabIndex++;
                    //element.Content = string.Format("{0},{1}", i, j);
                    //element.FontSize = 11d;
                    element.Click += new RoutedEventHandler(monthModeButton_Click);
                    this.monthUniformGrid.Children.Add(element);
                    this.MonthModeButtons[i - 2, j - 1] = element;
                }
            }

        }
        void monthModeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            var buttonDate = (PersianDate.PersianDate)button.Tag;
            var displayDate = this.DisplayDate;

            if (displayDate.Year != buttonDate.Year || displayDate.Month != buttonDate.Month)
                this.SetCurrentValue(DisplayDateProperty, new PersianDate.PersianDate(buttonDate.Year, buttonDate.Month, 1));
            this.SelectedDate = buttonDate;
        }
        #endregion


        #region Year
        private readonly Button[,] _yearModeButtons = new Button[4, 3];
        void initializeYear()
        {
            int tabIndex = 10;
            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var element = newControl();
                    element.Content = ((PersianDate.PersianMonth)j + i * 3 + 1).ToString();
                    //element.FontSize = 11d;
                    element.TabIndex = tabIndex++;
                    element.Click += new RoutedEventHandler(yearModeButton_Click);
                    element.Tag = j + i * 3 + 1;
                    this._yearModeButtons[i, j] = element;
                    this.yearUniformGrid.Children.Add(element);

                }
            }
        }
        void yearModeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            int month = (int)button.Tag;
            this.SetCurrentValue(DisplayDateProperty, new PersianDate.PersianDate(this.DisplayDate.Year, month, 1));
            this.DisplayMode = CalendarMode.Month;
        }
        #endregion


        #region Decade
        private readonly Button[] _decadeModeButtons = new Button[12];
        void initializeDecade()
        {
            int tabIndex = 10;

            this.decadeUniformGrid.Children.Add(new UIElement { IsEnabled = false });
            for (int j = 1; j <= 10; j++)
            {
                var element = newControl();
                element.TabIndex = tabIndex++;
                //element.FontSize = 11d;
                element.Click += new RoutedEventHandler(decadeModeButton_Click);
                element.Tag = j - 1;
                this._decadeModeButtons[j] = element;
                this.decadeUniformGrid.Children.Add(element);

            }
        }
        void decadeModeButton_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            this.SetCurrentValue(DisplayDateProperty, new PersianDate.PersianDate((int)button.Tag, 1, 1));
            this.DisplayMode = CalendarMode.Year;
        }
        #endregion

        private void selectedDateCheck(PersianDate.PersianDate? oldValue)
        {
            int r, c;

            if (this.SelectedDate is not null)
            {
                monthModeDateToRowColumn(this.SelectedDate.Value, out r, out c);
                setMonthModeButtonAppearance(this.MonthModeButtons[r, c]);
            }

            if (oldValue != null)
            {
                monthModeDateToRowColumn(oldValue.Value, out r, out c);
                setMonthModeButtonAppearance(this.MonthModeButtons[r, c]);
            }
        }
        void setMonthModeButtonAppearance(Button button)
        {
            Brush bg = Brushes.Transparent;
            if (button.Tag != null)
            {
                var bdate = (PersianDate.PersianDate)button.Tag;
                if (bdate == PersianDate.PersianDate.Today)
                {
                    bg = this.TodayBackground;
                }
                if (bdate == this.SelectedDate)
                {
                    bg = this.SelectedDateBackground;
                }
            }
            button.Background = bg;
        }
        private void todayCheck()
        {
            if (this.DisplayMode == CalendarMode.Month)
            {
                int r, c;
                monthModeDateToRowColumn(PersianDate.PersianDate.Today, out r, out c);
                setMonthModeButtonAppearance(this.MonthModeButtons[r, c]);
            }
        }
        /// <param name="row">zero-based row number</param>
        /// <param name="column">zero-based column number</param>
        private static void monthModeDateToRowColumn(PersianDate.PersianDate date, out int row, out int column)
        {
            int year = date.Year;
            int month = date.Month;
            PersianDate.PersianDate firstDay = new PersianDate.PersianDate(year, month, 1);
            int fstCol = 2 + (int)firstDay.PersianDayOfWeek;
            int fstRow = fstCol == 1 ? 2 : 1;
            row = (date.Day + fstCol - 2) / 7 + fstRow;
            column = (date.Day + fstCol - 1) % 7;
            column = column == 0 ? 7 : column;
            column--; row--;
        }
        /// <param name="row">zero-based row number</param>
        /// <param name="column">zero-based column number</param>
        private static PersianDate.PersianDate monthModeRowColumnToDate(int row, int column, PersianDate.PersianDate displayDate)
        {
            int year = displayDate.Year;
            int month = displayDate.Month;
            PersianDate.PersianDate firstDay = new PersianDate.PersianDate(year, month, 1);
            int fstCol = 2 + (int)firstDay.PersianDayOfWeek;
            int fstRow = fstCol == 1 ? 2 : 1;
            int dayDifference = (row) * 7 + column + 1 - ((fstRow - 1) * 7 + fstCol);
            return firstDay.AddDays(dayDifference);
        }

        private void setCalendar()
        {
            switch (this.DisplayMode)
            {
                case CalendarMode.Month:
                    setMonthMode();
                    break;
                case CalendarMode.Year:
                    setYearMode();
                    break;
                case CalendarMode.Decade:
                    setDecadeMode();
                    break;
                default:
                    throw new ArgumentOutOfRangeException("DisplayMode", "The DisplayMode value is not valid");
            }
        }
        private void setDecadeMode()
        {
            this.monthUniformGrid.Visibility = this.yearUniformGrid.Visibility = Visibility.Collapsed;
            this.decadeUniformGrid.Visibility = Visibility.Visible;

            int decade = DisplayDate.Year - DisplayDate.Year % 10;
            for (int i = 0; i < 10; i++)
            {
                int y = i + decade;
                if (y >= DisplayDateStart.Year && y <= DisplayDateEnd.Year)
                {
                    _decadeModeButtons[i + 1].Content = decade + i;
                    _decadeModeButtons[i + 1].Tag = decade + i;
                    _decadeModeButtons[i + 1].IsEnabled = true;

                }
                else
                {
                    _decadeModeButtons[i + 1].Content = "";
                    _decadeModeButtons[i + 1].Tag = null;
                    _decadeModeButtons[i + 1].IsEnabled = false;
                }
            }
            this.titleButton.Content = decade.ToString();
        }
        private void setMonthMode()
        {
            this.decadeUniformGrid.Visibility = this.yearUniformGrid.Visibility = Visibility.Collapsed;
            this.monthUniformGrid.Visibility = Visibility.Visible;

            int year = DisplayDate.Year;
            int month = DisplayDate.Month;
            PersianDate.PersianDate firstDayInMonth = new PersianDate.PersianDate(year, month, 1);
            for (int i = 1; i <= 6; i++)
            {
                for (int j = 1; j <= 7; j++)
                {
                    var button = MonthModeButtons[i - 1, j - 1];
                    PersianDate.PersianDate date = new PersianDate.PersianDate();
                    bool dateInRange;
                    try
                    {
                        //might throw OverflowException, which means that the date cannot be stored in PersianDate
                        date = monthModeRowColumnToDate(i - 1, j - 1, firstDayInMonth);
                        dateInRange = date >= DisplayDateStart && date <= DisplayDateEnd;
                    }
                    catch (OverflowException)
                    {
                        dateInRange = false;
                    }
                    if (dateInRange && date.Month == firstDayInMonth.Month)
                    {//we're good!
                        button.Foreground = Brushes.Black;
                        button.Content = date.Day.ToString();
                        button.IsEnabled = true;
                        button.Tag = date;
                    }
                    else if (dateInRange)
                    {//belongs to the next, or the previous month
                        button.Content = date.Day.ToString();
                        button.IsEnabled = true;
                        button.Tag = date;
                        button.Foreground = Brushes.LightGray;
                    }
                    else
                    {//not in [DiplayDateStart, DiplayDateEnd] range
                        button.Tag = null;
                        button.Content = "";
                        button.IsEnabled = false;
                        button.Background = Brushes.Transparent;
                    }
                }

            }

            this.titleButton.Content = ((PersianDate.PersianMonth)month).ToString() + " " + year.ToString();
            this.todayCheck();
            this.selectedDateCheck(null);
        }
        private void setYearMode()
        {
            this.monthUniformGrid.Visibility = this.decadeUniformGrid.Visibility = Visibility.Collapsed;
            this.yearUniformGrid.Visibility = Visibility.Visible;

            this.titleButton.Content = this.DisplayDate.Year.ToString();

            for (int i = 0; i < 4; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    int month = j + i * 3 + 1;
                    if (new PersianDate.PersianDate(DisplayDate.Year, month, PersianDate.PersianDate.DaysInMonth(DisplayDate.Year, month)) >= DisplayDateStart &&
                        new PersianDate.PersianDate(DisplayDate.Year, month, 1) <= DisplayDateEnd)
                    {
                        _yearModeButtons[i, j].Content = ((PersianDate.PersianMonth)month).ToString();
                        _yearModeButtons[i, j].IsEnabled = true;
                    }
                    else
                    {
                        _yearModeButtons[i, j].Content = "";
                        _yearModeButtons[i, j].IsEnabled = false;
                    }
                }
            }
        }

        private void nextButton_Click(object sender, RoutedEventArgs e)
        {
            int m = this.DisplayDate.Month;
            int y = this.DisplayDate.Year;
            try
            {
                PersianDate.PersianDate newDisplayDate = DisplayDate;
                if (this.DisplayMode == CalendarMode.Month)
                {
                    if (m == 12)
                        newDisplayDate = new PersianDate.PersianDate(y + 1, 1, 1);
                    else
                        newDisplayDate = new PersianDate.PersianDate(y, m + 1, 1);
                }
                else if (this.DisplayMode == CalendarMode.Year)
                {
                    newDisplayDate = new PersianDate.PersianDate(DisplayDate.Year + 1, 1, 1);
                }
                else if (this.DisplayMode == CalendarMode.Decade)
                {
                    newDisplayDate = new PersianDate.PersianDate(y - y % 10 + 10, 1, 1);
                }

                if (newDisplayDate >= DisplayDateStart && newDisplayDate <= DisplayDateEnd)
                    this.SetCurrentValue(DisplayDateProperty, newDisplayDate);
            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        private void previousButton_Click(object sender, RoutedEventArgs e)
        {
            int m = this.DisplayDate.Month;
            int y = this.DisplayDate.Year;
            try
            {
                PersianDate.PersianDate newDisplayDate = DisplayDate;

                if (this.DisplayMode == CalendarMode.Month)
                {
                    if (m == 1)
                        newDisplayDate = new PersianDate.PersianDate(y - 1, 12, PersianDate.PersianDate.DaysInMonth(y - 1, 12));
                    else
                        newDisplayDate = new PersianDate.PersianDate(y, m - 1, PersianDate.PersianDate.DaysInMonth(y, m - 1));
                }
                else if (this.DisplayMode == CalendarMode.Year)
                {
                    newDisplayDate = new PersianDate.PersianDate(y - 1, 12, PersianDate.PersianDate.DaysInMonth(y - 1, 12));
                }
                else if (this.DisplayMode == CalendarMode.Decade)
                {
                    newDisplayDate = new PersianDate.PersianDate(y - y % 10 - 1, 12, PersianDate.PersianDate.DaysInMonth(y - y % 10 - 1, 12));
                }

                if (newDisplayDate >= DisplayDateStart && newDisplayDate <= DisplayDateEnd)
                    this.SetCurrentValue(DisplayDateProperty, newDisplayDate);
            }
            catch (ArgumentOutOfRangeException)
            {
            }
        }

        private void titleButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.DisplayMode == CalendarMode.Month)
                this.DisplayMode = CalendarMode.Year;
            else if (this.DisplayMode == CalendarMode.Year)
                this.DisplayMode = CalendarMode.Decade;
        }

        private void GoToToday_Button_Click(object sender, RoutedEventArgs e)
        {
            var newDisplayDate = new PersianDate.PersianDate(DateTime.Now);

            if (newDisplayDate >= DisplayDateStart && newDisplayDate <= DisplayDateEnd)
            {
                this.SetCurrentValue(DisplayDateProperty, newDisplayDate);
                this.SelectedDate = newDisplayDate;
            }
        }

        private void ClearDate_Button_Click(object sender, RoutedEventArgs e)
        {
            this.SelectedDate = null;
        }
    }
}
