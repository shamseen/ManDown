using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using BluetoothLE.Core;
using System;
using ManDown.Pages;
using ManDown.Database;
using ManDown.Models;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ManDown
{
    public partial class App : Application
    {
        private static readonly IAdapter _bluetoothAdapter;
        public static IAdapter BluetoothAdapter { get { return _bluetoothAdapter; } }
        public static IList<string> PhoneNumbers { get; set; }

        public static Person Patient { get; set; }

        public static Person Emergency { get; set; }

        static ContactsDb database;

        public static ContactsDb Database
        {
            get
            {
                if (database == null)
                {
                    database = new ContactsDb(DependencyService.Get<IFileHelper>().GetLocalFilePath("ManDownSQLite.db3"));
                }
                return database;
            }
        }

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

            //style
            defineStyles();

            if (App.Patient == null || App.Emergency == null)
            {
                App.Patient = new Person();
                App.Emergency = new Person();
                MainPage = new NavigationPage(new InsertInfo());
            }

            else
            {
                MainPage = new NavigationPage(new MainPage());
            }
        }

        void defineStyles()
        {
            Current.Resources = new ResourceDictionary();

            var pageStyle = new Style(typeof(TemplatedPage))
            {
                Setters = { new Setter() { Property = TemplatedPage.BackgroundColorProperty,
                Value = Color.FromHex("#707070") } }
            };

            Current.Resources.Add(pageStyle);   
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
}