using Final_Xamarin_App.Models;
using System;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsContentPage : ContentPage
    {
        public NewsContentPage(News news)
        { 
            if(news == null)
            {
                throw new ArgumentNullException();
            }

            BindingContext = news;
            InitializeComponent();
        }
    }
}