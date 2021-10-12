using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SignupPage : ContentPage
    {
        public SignupPage()
        {
            InitializeComponent();
        }

        async void Button_Clicked(System.Object sender, System.EventArgs e)
        {
            var result = await MediaPicker.PickPhotoAsync(new MediaPickerOptions
            {
                Title = "Please Pick a photo"
            });
            var stream = await result.OpenReadAsync();
            resultImage.Source = ImageSource.FromStream(() => stream);
        }
        async void Back_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopAsync();

        }

        async void SignUp_Button_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new HomePage());
        }
    }
}