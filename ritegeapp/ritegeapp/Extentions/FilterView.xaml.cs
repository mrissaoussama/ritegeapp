using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ritegeapp.Extentions
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FilterView : Frame
    {
        public FilterView()
        {
            InitializeComponent();
        }
        public DateTime DateStart
        {
            get => (DateTime)GetValue(DateStartProperty);
            set => SetValue(DateStartProperty, value);
        }

        public static readonly BindableProperty DateStartProperty = BindableProperty.Create
             ("DateStart", typeof(DateTime), typeof(FilterView), DateTime.Today, BindingMode.TwoWay, propertyChanged: DateStartPropertyChanged);
        private static void DateStartPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.DateStartPicker.Date = (DateTime)newValue;
            control.DateEndLabel.MinimumDate = (DateTime)newValue;

        }
        private static void DateEndPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.DateEndLabel.Date = (DateTime)newValue;
        }

        public DateTime DateEnd
        {
            get => (DateTime)GetValue(DateEndProperty);
            set => SetValue(DateEndProperty, value);
        }

        public static readonly BindableProperty DateEndProperty = BindableProperty.Create
           ("DateEnd", typeof(DateTime), typeof(FilterView), DateTime.Today.AddDays(1).AddTicks(-1), BindingMode.TwoWay, propertyChanged: DateEndPropertyChanged);


        public string SearchSubjectText
        {
            get => (string)GetValue(SearchSubjectTextProperty);
            set => SetValue(SearchSubjectTextProperty, value);
        }

        public static readonly BindableProperty SearchSubjectTextProperty = BindableProperty.Create
            ("SearchSubjectText", typeof(string), typeof(FilterView), "", BindingMode.TwoWay, propertyChanged: SearchSubjectChanged);


        public string SearchTextBoxEntry
        {
            get => (string)GetValue(SearchTextBoxEntryProperty);
            set => SetValue(SearchTextBoxEntryProperty, value);
        }

        public static readonly BindableProperty SearchTextBoxEntryProperty = BindableProperty.Create
            ("SearchTextBox", typeof(string), typeof(FilterView), "", BindingMode.TwoWay, propertyChanged: SearchTextBoxPropertyChanged);

        public bool CanTapSearchTextBool
        {
            get => (bool)GetValue(CanTapSearchTextBoolProperty);
            set => SetValue(CanTapSearchTextBoolProperty, value);
        }
        public static readonly BindableProperty CanTapSearchTextBoolProperty = BindableProperty.Create
            ("CanTapSearchText", typeof(bool), typeof(FilterView), false, BindingMode.TwoWay, propertyChanged: CanTapSearchTextChanged);
        public string CanClearFilterBool
        {
            get => (string)GetValue(CanClearFilterBoolProperty);
            set => SetValue(CanClearFilterBoolProperty, value);
        }
        public static readonly BindableProperty CanClearFilterBoolProperty = BindableProperty.Create
            ("CanClearFilter", typeof(bool), typeof(FilterView), false, BindingMode.TwoWay, propertyChanged: CanClearFilterChanged);
        public string CanSortBool
        {
            get => (string)GetValue(CanSortBoolProperty);
            set => SetValue(CanSortBoolProperty, value);
        }
        public static readonly BindableProperty CanSortBoolProperty = BindableProperty.Create
            ("CanSort", typeof(bool), typeof(FilterView), false, BindingMode.TwoWay, propertyChanged: CanSortChanged);




        public static readonly BindableProperty CanTapSearchTextCommandProperty =
       BindableProperty.Create("CanTapSearchTextCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.TwoWay, propertyChanged: CanTapSearchTextCommandChanged);

        public static readonly BindableProperty CanClearFilterCommandProperty =
       BindableProperty.Create("CanClearFilterCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.TwoWay, propertyChanged: CanClearFilterCommandChanged);
        public static readonly BindableProperty CanSortCommandProperty =
    BindableProperty.Create("CanSortCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.TwoWay, propertyChanged: CanSortCommandChanged);

        public Command CanTapSearchTextCommand
        {
            get { return (Command)GetValue(CanTapSearchTextCommandProperty); }
            set { SetValue(CanTapSearchTextCommandProperty, value); }
        }
        public Command CanClearFilterCommand
        {
            get { return (Command)GetValue(CanClearFilterCommandProperty); }
            set { SetValue(CanClearFilterCommandProperty, value); }
        }
        public Command CanSortCommand
        {
            get { return (Command)GetValue(CanSortCommandProperty); }
            set { SetValue(CanSortCommandProperty, value); }
        }
   
        private static void SearchTextBoxPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.SearchTextBoxField.Text = (string)newValue;
        }
        private static void SearchSubjectChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.SearchSubject.Text = (string)newValue;
        }
        private static void CanTapSearchTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.CanTapSearchText.IsEnabled = (bool)newValue;
        }
        private static void CanClearFilterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.CanClearFilter.IsEnabled = (bool)newValue;
        }
        private static void CanSortChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.CanSort.IsEnabled = (bool)newValue;
        }
        private static void CanTapSearchTextCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            var command = (CommunityToolkit.Mvvm.Input.RelayCommand<object>)newValue;
            TouchEffect.SetCommand(control.CanTapSearchText, command);
        }
        private static void CanClearFilterCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            var command = (CommunityToolkit.Mvvm.Input.RelayCommand<object>)newValue;

            TouchEffect.SetCommand(control.CanClearFilter, command);
        }
        private static void CanSortCommandChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            var command = (CommunityToolkit.Mvvm.Input.RelayCommand<object>)newValue;

            TouchEffect.SetCommand(control.CanSort, command);
        }
       
    }
}