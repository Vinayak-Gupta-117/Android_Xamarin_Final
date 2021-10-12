using Final_Xamarin_App.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Final_Xamarin_App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NewsPage : ContentPage
    {

        private const string Url = "https://hacker-news.firebaseio.com/v0/topstories.json?print=pretty";
        private const string halfNewsUrl = "https://hacker-news.firebaseio.com/v0/item/";
        private HttpClient _client = new HttpClient();
        private List<String> _lists;
        private ObservableCollection<News> _news = new ObservableCollection<News>();
        public NewsPage()
        {
            InitializeComponent();
        }

        async protected override void OnAppearing()
        {
            var list = await _client.GetStringAsync(Url);
            var newsId = JsonConvert.DeserializeObject<List<string>>(list);
            _lists = new List<string>(newsId);

            if (_news.Count == 0)
            {
                AddNews();
            }
            listView.ItemsSource = _news;
            base.OnAppearing();
        }

        async private void AddNews()
        {
            for (int i = 0; i < 30; i++)
            {
                string newsUrl = halfNewsUrl + _lists[i] + ".json?print=pretty";
                var JsonNewsContent = await _client.GetStringAsync(newsUrl);
                var newsContent = JsonConvert.DeserializeObject<News>(JsonNewsContent);
                _news.Add(newsContent);
            }
        }

        async private  void listView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if(e.SelectedItem == null)
            {
                return;
            }
            var news = e.SelectedItem as News;
            await Navigation.PushAsync(new NewsContentPage(news));

            listView.SelectedItem = null;
        }

        private void Title_Clicked(object sender, EventArgs e)
        {
            var menuItem = sender as MenuItem;
            var news = menuItem.CommandParameter as News;

            DisplayAlert("News Title", news.title, "OK");
        }

        async private void Delete_Clicked(object sender, EventArgs e)
        {
            var response = await DisplayAlert("Warning", "Are you sure?", "Yes", "No");
            if (response)
            {
                var news = (sender as MenuItem).CommandParameter as News;
                _news.Remove(news);
                await DisplayAlert("Done", "Your response will be saved", "OK");
            }
            else
            {
                return;
            }
        }

        IEnumerable<News> GetNews(string searchText = null)
        {
            
            
            if (String.IsNullOrWhiteSpace(searchText))
            {
                return _news;
            }

            return _news.Where(c => c.title.StartsWith(searchText));
        }

        private void listView_Refreshing(object sender, EventArgs e)
        {
            listView.ItemsSource = GetNews();
            listView.IsRefreshing = false;
            //listView.EndRefresh();
        }

        private void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            listView.ItemsSource =  GetNews(e.NewTextValue);
        }
    }
}