using ManDown.Models;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ManDown.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class InsertInfo : ContentPage
    {
        public InsertInfo()
        {
            InitializeComponent();
        }

        async void OnSubmit(object sender, EventArgs e)
        {
            var newPatient = new Person(patientFirst.Text, patientLast.Text, patientPhone.Text, 0);
            await App.Database.DeleteItemAsync(App.Patient);
            App.Patient = newPatient;
            await App.Database.SaveItemAsync(App.Patient);

            //var newEmergency = new Person(contactFirst.Text, contactLast.Text, contactPhone.Text, 1);
            //await App.Database.DeleteItemAsync(App.Emergency);
            //App.Emergency = newEmergency;
            //await App.Database.SaveItemAsync(App.Emergency);

            //nav to main page
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}
