using CommunityToolkit.Maui.Alerts;
using OpenWeatherApp.Entities;
using OpenWeatherApp.Models;
using OpenWeatherApp.Services;
using System.Net;

namespace OpenWeatherApp.Pages;

public partial class SearchCitiesPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _apiKey;

    public SearchCitiesPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
        _apiKey = Preferences.Get(AppSettings.APIKey, string.Empty);
        EntrySearchCity.ReturnCommand = new Command(SearchCities);
    }

    private async void SearchCities()
    {
        var response = await _apiService.GetRequestAsync<List<Geocode>>($"/geo/1.0/direct?q={EntrySearchCity.Text}&limit=5&appid={_apiKey}");
        if (response.Success)
        {
            List<Geocode> geocodes = response.Data!;
            if (geocodes.Count == 0)
            {
                CollectionCities.ItemsSource = null;
                ViewNoResults.IsVisible = true;
            }
            else
            {
                CollectionCities.ItemsSource = geocodes;
                ViewNoResults.IsVisible = false;
            }
            return;
        }

        // Failed to load
        switch (response.StatusCode)
        {
            // API key invalid, moving to login page
            case (int)HttpStatusCode.Unauthorized:
                Application.Current!.MainPage = new NavigationPage(new LoginPage(_apiService));
                return;

            // API key is temporarily locked due to too many requests
            case (int)HttpStatusCode.TooManyRequests:
                await Toast.Make("Your API key surpassed the request limit").Show();
                return;

            // No internet connection
            case -1:
                if (await DisplayAlert("Network Error", "Check the internet connection", "Retry", "Exit"))
                {
                    SearchCities();
                }
                else
                {
                    Application.Current!.Quit();
                }
                return;
            default:
                await Toast.Make("An unexpected error occurred").Show();
                return;
        }
    }

    private void CollectionCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BtnSelect.IsEnabled = e.CurrentSelection != null;
    }

    private void Select(object sender, EventArgs e)
    {
        var selected = CollectionCities.SelectedItem as Geocode;
        if (selected != null)
        {
            Preferences.Set(AppSettings.FirstTime, false);
            Preferences.Set(AppSettings.SelectedCity, new GeocodeCity(selected).Serialize());
            if (Application.Current!.MainPage is AppShell)
            {
                Navigation.PopToRootAsync();
                return;
            }
            Application.Current!.MainPage = new AppShell(_apiService);
            return;
        }
        Toast.Make("Something wrong happened").Show();
    }
}