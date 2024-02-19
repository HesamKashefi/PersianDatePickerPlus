using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;

namespace Mohsen.PersianDateControls
{
    /// <summary>
    /// A Fully Customizable Persian DatePicker
    /// </summary>
    [DefaultEvent("SelectedDateChanged")]
    [DefaultProperty("SelectedDate")]
    [TemplatePart(Name = OpenCalendarButtonElementPartName, Type = typeof(ButtonBase))]
    [TemplatePart(Name = DatePickerPopupElementPartName, Type = typeof(Popup))]
    [TemplatePart(Name = PersianCalendarElementPartName, Type = typeof(PersianCalendar))]
    [TemplatePart(Name = DateTextBoxElementPartName, Type = typeof(TextBox))]
    public class PersianDatePicker : Control
    {
        #region PART Names
        public const string OpenCalendarButtonElementPartName = "PART_OpenCalendarButtonElement";
        public const string DatePickerPopupElementPartName = "PART_DatePickerPopupElement";
        public const string PersianCalendarElementPartName = "PART_PersianCalendarElement";
        public const string DateTextBoxElementPartName = "PART_DateTextBoxElement";
        #endregion

        #region PARTS
        protected ButtonBase _openCalendarButtonElement;
        protected Popup _datePickerPopupElement;
        protected PersianCalendar _persianCalendarElement;
        protected TextBox _dateTextBoxElement;
        #endregion

        #region Method Overrides
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _openCalendarButtonElement = GetTemplateChild(OpenCalendarButtonElementPartName) as ButtonBase ??
                throw new ArgumentNullException($"PersianDatePicker Missing PART: '{OpenCalendarButtonElementPartName}'");

            _datePickerPopupElement = GetTemplateChild(DatePickerPopupElementPartName) as Popup ??
                throw new ArgumentNullException($"PersianDatePicker Missing PART: '{DatePickerPopupElementPartName}'");

            _persianCalendarElement = GetTemplateChild(PersianCalendarElementPartName) as PersianCalendar ??
                throw new ArgumentNullException($"PersianDatePicker Missing PART: '{PersianCalendarElementPartName}'");

            _dateTextBoxElement = GetTemplateChild(DateTextBoxElementPartName) as TextBox ??
                throw new ArgumentNullException($"PersianDatePicker Missing PART: '{DateTextBoxElementPartName}'");

            SetBindings();

            _openCalendarButtonElement.Click += OpenCalendarButtonElement_Click;
            _datePickerPopupElement.Opened += DatePickerPopupElement_Opened;
            _persianCalendarElement.SelectedDateChanged += PersianCalendarElement_SelectedDateChanged;
            _dateTextBoxElement.LostFocus += DateTextBoxElement_LostFocus;
            _dateTextBoxElement.KeyUp += DateTextBoxElement_KeyUp;
        }
        #endregion

        #region Dependency Properties

        [Category("Date Picker")]
        public string PlaceHolder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }
        public static readonly DependencyProperty PlaceHolderProperty =
            DependencyProperty.Register("PlaceHolder", typeof(string), typeof(PersianDatePicker), new PropertyMetadata("انتخاب تاریخ"));


        [Category("Date Picker")]
        public Mohsen.PersianDate? SelectedDate
        {
            get { return (Mohsen.PersianDate?)GetValue(SelectedDateProperty); }
            set { SetValue(SelectedDateProperty, value); }
        }
        public static readonly DependencyProperty SelectedDateProperty;

        /// <summary>
        /// Gets or sets the date that is being displayed in the calendar.
        /// </summary>
        [Category("Date Picker")]
        public Mohsen.PersianDate DisplayDate
        {
            get { return (Mohsen.PersianDate)GetValue(DisplayDateProperty); }
            set { SetValue(DisplayDateProperty, value); }
        }
        public static readonly DependencyProperty DisplayDateProperty;

        /// <summary>
        /// the minimum date that is displayed, and can be selected
        /// </summary>
        [Category("Date Picker")]
        public Mohsen.PersianDate DisplayDateStart
        {
            get { return (Mohsen.PersianDate)GetValue(DisplayDateStartProperty); }
            set { SetValue(DisplayDateStartProperty, value); }
        }
        public static readonly DependencyProperty DisplayDateStartProperty;


        /// <summary>
        /// the maximum date that is displayed, and can be selected
        /// </summary>
        [Category("Date Picker")]
        public Mohsen.PersianDate DisplayDateEnd
        {
            get { return (Mohsen.PersianDate)GetValue(DisplayDateEndProperty); }
            set { SetValue(DisplayDateEndProperty, value); }
        }
        public static readonly DependencyProperty DisplayDateEndProperty;


        public string Text
        {
            get { return (string)GetValue(TextProperty); }
            set { SetValue(TextProperty, value); }
        }
        public static readonly DependencyProperty TextProperty;

        //events

        public static readonly RoutedEvent SelectedDateChangedEvent;
        public event RoutedEventHandler SelectedDateChanged
        {
            add { AddHandler(SelectedDateChangedEvent, value); }
            remove { RemoveHandler(SelectedDateChangedEvent, value); }
        }

        //property changed callbacks and value coercions
        static object coerceDisplayDateEnd(DependencyObject d, object o)
        {
            var pdp = d as PersianDatePicker;
            if (pdp is null) return null;
            Mohsen.PersianDate value = (Mohsen.PersianDate)o;
            if (value < pdp.DisplayDateStart)
            {
                return pdp.DisplayDateStart;
            }
            return o;
        }
        static object coerceDateToBeInRange(DependencyObject d, object o)
        {
            PersianDatePicker pdp = d as PersianDatePicker;
            if (o is null) return null;
            Mohsen.PersianDate value = (Mohsen.PersianDate)o;
            if (value < pdp.DisplayDateStart)
            {
                return pdp.DisplayDateStart;
            }
            if (value > pdp.DisplayDateEnd)
            {
                return pdp.DisplayDateEnd;
            }
            return o;
        }

        static void selectedDateChanged(DependencyObject o, DependencyPropertyChangedEventArgs e)
        {
            PersianDatePicker pdp = o as PersianDatePicker;
            if (pdp is not null)
            {
                pdp.Text = e.NewValue?.ToString() ?? string.Empty;
                pdp.RaiseEvent(new RoutedEventArgs(SelectedDateChangedEvent, pdp));
            }
        }

        #endregion

        #region Ctor

        public PersianDatePicker()
        {
            this.Text = this.SelectedDate.ToString();
        }

        static PersianDatePicker()
        {
            PropertyMetadata selectedDateMetadata = new PropertyMetadata(Mohsen.PersianDate.Today, selectedDateChanged);
            selectedDateMetadata.CoerceValueCallback = coerceDateToBeInRange;
            SelectedDateProperty =
                DependencyProperty.Register("SelectedDate", typeof(Mohsen.PersianDate?), typeof(PersianDatePicker), selectedDateMetadata);

            PropertyMetadata displayDateMetadata = new PropertyMetadata(Mohsen.PersianDate.Today);
            displayDateMetadata.CoerceValueCallback = coerceDateToBeInRange;
            DisplayDateProperty =
                DependencyProperty.Register("DisplayDate", typeof(Mohsen.PersianDate), typeof(PersianDatePicker), displayDateMetadata);

            PropertyMetadata textMetadata = new PropertyMetadata(Mohsen.PersianDate.Today.ToString());
            TextProperty = DependencyProperty.Register("Text", typeof(string), typeof(PersianDatePicker), textMetadata);

            PropertyMetadata displayDateStartMetaData = new PropertyMetadata(new Mohsen.PersianDate());
            DisplayDateStartProperty =
                DependencyProperty.Register("DisplayDateStart", typeof(Mohsen.PersianDate), typeof(PersianDatePicker), displayDateStartMetaData);

            PropertyMetadata displayDateEndMetaData = new PropertyMetadata(new Mohsen.PersianDate(10000, 1, 1));
            displayDateEndMetaData.CoerceValueCallback = coerceDisplayDateEnd;
            DisplayDateEndProperty =
                DependencyProperty.Register("DisplayDateEnd", typeof(Mohsen.PersianDate), typeof(PersianDatePicker), displayDateEndMetaData);

            SelectedDateChangedEvent =
                EventManager.RegisterRoutedEvent("SelectedDateChanged", RoutingStrategy.Bubble, typeof(RoutedEventHandler), typeof(PersianDatePicker));
        }
        #endregion

        #region Properties
        /// <summary>
        /// This readonly property gets the PersianCalendar object displayed when clicking the Calendar button.
        /// </summary>
        public PersianCalendar PersianCalendar
        {
            get { return _persianCalendarElement; }
        }
        #endregion

        #region Event Handlers
        private void OpenCalendarButtonElement_Click(object sender, RoutedEventArgs e)
        {
            this._datePickerPopupElement.IsOpen = true;
        }

        private void PersianCalendarElement_SelectedDateChanged(object sender, RoutedEventArgs e)
        {
            this._datePickerPopupElement.IsOpen = false;
        }

        private void DateTextBoxElement_LostFocus(object sender, RoutedEventArgs e)
        {
            ValidateText();
        }
        private void DateTextBoxElement_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
                ValidateText();
        }

        private void DatePickerPopupElement_Opened(object sender, EventArgs e)
        {
            this._persianCalendarElement.Focus();
        }
        #endregion

        #region Private Methods
        private void SetBindings()
        {
            Binding selectedDateBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("SelectedDate"),
                Mode = BindingMode.TwoWay,
            };
            this._persianCalendarElement.SetBinding(PersianCalendar.SelectedDateProperty, selectedDateBinding);

            Binding displayDateBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("DisplayDate"),
                Mode = BindingMode.TwoWay,
            };
            this._persianCalendarElement.SetBinding(PersianCalendar.DisplayDateProperty, displayDateBinding);

            Binding textBinding = new Binding
            {
                Source = this,
                Path = new PropertyPath("Text"),
                Mode = BindingMode.TwoWay,
            };
            this._dateTextBoxElement.SetBinding(TextBox.TextProperty, textBinding);

            Binding displayDateStartBinding = new Binding
            {
                Source = this._persianCalendarElement,
                Path = new PropertyPath("DisplayDateStart"),
                Mode = BindingMode.TwoWay,
            };
            this.SetBinding(DisplayDateStartProperty, displayDateStartBinding);

            Binding displayDateEndBinding = new Binding
            {
                Source = this._persianCalendarElement,
                Path = new PropertyPath("DisplayDateEnd"),
                Mode = BindingMode.TwoWay,
            };
            this.SetBinding(DisplayDateEndProperty, displayDateEndBinding);
        }

        private void ValidateText()
        {
            if (Mohsen.PersianDate.TryParse(this._dateTextBoxElement.Text, out Mohsen.PersianDate date))
            {
                this.SelectedDate = date;
                this.DisplayDate = date;
            }
            this.Text = this.SelectedDate?.ToString() ?? string.Empty;
        }
        #endregion
    }
}
