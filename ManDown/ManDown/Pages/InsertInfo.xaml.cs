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
            App.Patient.FirstName = patientFirst.Text;
            App.Patient.LastName = patientLast.Text;
            App.Patient.PhoneNumber = patientPhone.Text;
            App.Contact.FirstName = contactFirst.Text;
            App.Contact.LastName = contactLast.Text;
            App.Contact.PhoneNumber = contactPhone.Text;

            //nav to main page
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}
