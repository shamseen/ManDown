using System;
using Xamarin.Forms;
using ManDown.Pages;
using Plugin.Messaging;
using Plugin.Geolocator;
using System.Linq;
using Xamarin.Forms.Maps;

namespace ManDown
{
    public partial class MainPage : ContentPage
    {
        string translatedNumber;

        public MainPage()
        {
            InitializeComponent();
        }

        async void Login()
        {
            await Navigation.PushAsync(new InsertInfo());
        }

        /// <summary>
        /// FOR TESTING ONLY! Fakes alert because I don't have the Arduino
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnAlert(object sender, EventArgs e)
        {
            //Get Location
            var locator = CrossGeolocator.Current;
            var location = await locator.GetPositionAsync(timeoutMilliseconds: 10000);
            string mapURL = String.Format("http://www.google.com/maps/place/{0},{1}", location.Latitude, location.Longitude);

            // Send Sms
            string msg = String.Format("MAN DOWN: {1} {2} fell! Location: {0}", mapURL, App.Patient.FirstName, App.Patient.LastName);
            var smsMessenger = CrossMessaging.Current.SmsMessenger;
            if (smsMessenger.CanSendSmsInBackground)
                smsMessenger.SendSmsInBackground(App.Contact.PhoneNumber, msg);

        }

        /// <summary>
        /// When "Connect" is clicked, will navigate to DeviceListPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnConnect(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DeviceListPage(App.BluetoothAdapter) { Title = "Connect Bluetooth" });
        }

        ///// <summary>
        ///// When "Translate" btn is clicked, will translate whatever is in the Entry element.
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //void OnTranslate(object sender, EventArgs e)
        //{
        //    translatedNumber = Core.ManDownService.ToNumber(phoneNumberText.Text);
        //    if (!string.IsNullOrWhiteSpace(translatedNumber))
        //    {
        //        callButton.IsEnabled = true;
        //        callButton.Text = "Call " + translatedNumber;
        //    }
        //    else
        //    {
        //        callButton.IsEnabled = false;
        //        callButton.Text = "Call";
        //    }
        //}

        /// <summary>
        /// When "Call" btn is clicked, will render pop-up to confirm calling this #.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnCall(object sender, EventArgs e)
        {
            if (await this.DisplayAlert(
                    "Dial a Number",
                    "Would you like to call " + translatedNumber + "?",
                    "Yes",
                    "No"))
            {
                var dialer = DependencyService.Get<IDialer>();
                if (dialer != null)
                {
                    App.PhoneNumbers.Add(translatedNumber);
                    historyButton.IsEnabled = true;
                    dialer.Dial(translatedNumber);
                }
            }
        }

        /// <summary>
        /// When "History" btn is clicked, will navigate to History page.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        async void OnCallHistory(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new HistoryPage());
        }
    }
}