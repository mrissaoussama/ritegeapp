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
        public DateTime dateStart
        {
            get => (DateTime)GetValue(dateStartProperty);
            set => SetValue(dateStartProperty, value);
        }

        public static readonly BindableProperty dateStartProperty = BindableProperty.Create
             ("dateStart", typeof(DateTime), typeof(FilterView), DateTime.Now, BindingMode.OneWay, propertyChanged: dateStartPropertyChanged);
        private static void dateStartPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.dateStartLabel.Date = (DateTime)newValue;
            control.DateDFinLabel.MinimumDate = (DateTime)newValue;

        }
        private static void dateEndPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var control = (FilterView)bindable;
            control.DateDFinLabel.Date = (DateTime)newValue;
        }

        public DateTime dateEnd
        {
            get => (DateTime)GetValue(dateEndProperty);
            set => SetValue(dateEndProperty, value);
        }

        public static readonly BindableProperty dateEndProperty = BindableProperty.Create
           ("dateEnd", typeof(DateTime), typeof(FilterView), DateTime.Now, BindingMode.OneWay, propertyChanged: dateEndPropertyChanged);


        public string SearchSubjectText
        {
            get => (string)GetValue(SearchSubjectTextProperty);
            set => SetValue(SearchSubjectTextProperty, value);
        }

        public static readonly BindableProperty SearchSubjectTextProperty = BindableProperty.Create
            ("SearchSubjectText", typeof(string), typeof(FilterView), "", BindingMode.OneWay, propertyChanged: SearchSubjectChanged);


        public string SearchTextBoxEntry
        {
            get => (string)GetValue(SearchTextBoxEntryProperty);
            set => SetValue(SearchTextBoxEntryProperty, value);
        }

        public static readonly BindableProperty SearchTextBoxEntryProperty = BindableProperty.Create
            ("SearchTextBoxEntryText", typeof(string), typeof(FilterView), "", BindingMode.OneWay, propertyChanged: SearchTextBoxEntryPropertyChanged);

        public bool CanTapSearchTextBool
        {
            get => (bool)GetValue(CanTapSearchTextBoolProperty);
            set => SetValue(CanTapSearchTextBoolProperty, value);
        }
        public static readonly BindableProperty CanTapSearchTextBoolProperty = BindableProperty.Create
            ("CanTapSearchText", typeof(bool), typeof(FilterView), false, BindingMode.OneWay, propertyChanged: CanTapSearchTextChanged);
        public string CanClearFilterBool
        {
            get => (string)GetValue(CanClearFilterBoolProperty);
            set => SetValue(CanClearFilterBoolProperty, value);
        }
        public static readonly BindableProperty CanClearFilterBoolProperty = BindableProperty.Create
            ("CanClearFilter", typeof(bool), typeof(FilterView), false, BindingMode.OneWay, propertyChanged: CanClearFilterChanged);
        public string CanSortBool
        {
            get => (string)GetValue(CanSortBoolProperty);
            set => SetValue(CanSortBoolProperty, value);
        }
        public static readonly BindableProperty CanSortBoolProperty = BindableProperty.Create
            ("CanSort", typeof(bool), typeof(FilterView), false, BindingMode.OneWay, propertyChanged: CanSortChanged);




        public static readonly BindableProperty CanTapSearchTextCommandProperty =
       BindableProperty.Create("CanTapSearchTextCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.OneWay, propertyChanged: CanTapSearchTextCommandChanged);

        public static readonly BindableProperty CanClearFilterCommandProperty =
       BindableProperty.Create("CanClearFilterCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.OneWay, propertyChanged: CanClearFilterCommandChanged);
        public static readonly BindableProperty CanSortCommandProperty =
    BindableProperty.Create("CanSortCommand", typeof(IRelayCommand), typeof(CommunityToolkit.Mvvm.Input.RelayCommand), default(ICommand), BindingMode.OneWay, propertyChanged: CanSortCommandChanged);

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
   
        private static void SearchTextBoxEntryPropertyChanged(BindableObject bindable, object oldValue, object newValue)
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