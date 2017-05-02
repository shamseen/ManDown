using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BluetoothLE.Core;
using System;
using ManDown.Pages;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ManDown
{
    public partial class App : Application
    {
        private static readonly IAdapter _bluetoothAdapter;
        public static IAdapter BluetoothAdapter { get { return _bluetoothAdapter; } }
        public static IList<string> PhoneNumbers { get; set; }

        public static Person Patient { get; set; }

        public static Person Contact { get; set; }

        static App()
        {
            _bluetoothAdapter = DependencyService.Get<IAdapter>();

            _bluetoothAdapter.ScanTimeout = TimeSpan.FromSeconds(10);
            _bluetoothAdapter.ConnectionTimeout = TimeSpan.FromSeconds(10);
        }

        public App()
        {
            InitializeComponent();
            PhoneNumbers = new List<string>();
            if (App.Patient == null || App.Contact == null)
            {
                App.Patient = new Person();
                App.Contact = new Person();
                MainPage = new NavigationPage(new InsertInfo());
            }

            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }

    public class Person
    {
        public string FirstName;
        public string LastName;
        public string PhoneNumber;

        public Person() { }

        public Person(string first, string last, string phone)
        {
            FirstName = first;
            LastName = last;
            PhoneNumber = phone;
        }
    }
}