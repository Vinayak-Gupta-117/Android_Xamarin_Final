using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Final_Xamarin_App
{
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
        }

        async private void loginbtn_Clicked(object sender, EventArgs e)
        {
            //await Navigation.PushAsync(new SignupPage());
            DisplayAlert("No record found", "Create a new account because no record was found", "Ok");
        }

        async private void signupbtn_clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new SignupPage());
        }
    }
}
