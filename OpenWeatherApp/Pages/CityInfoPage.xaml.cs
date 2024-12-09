using OpenWeatherApp.Entities;
using OpenWeatherApp.Services;

namespace OpenWeatherApp.Pages;

public partial class CityInfoPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _apiKey;

    private string _currentGeoCity = string.Empty;

    public CityInfoPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
        _apiKey = Preferences.Get(AppSettings.APIKey, string.Empty);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Get GeoCity
        var city = Preferences.Get(AppSettings.SelectedCity, null);

        // Something wrong happened, act as if it's the first time
        if (string.IsNullOrEmpty(city))
        {
            Preferences.Set(AppSettings.FirstTime, true);
            Application.Current!.MainPage = new NavigationPage(new FirstTimePage(_apiService));
            return;
        }

        if (city.Equals(_currentGeoCity)) return;
        _currentGeoCity = city;

        SetCityValues(IGeoCity.Deserialize(city)!);
    }

    private void SetCityValues(IGeoCity city)
    {
        LabelCityName.Text = city.Name;
        LabelCountryName.Text = city.CountryName;
    }

    private void ChangeCity(object sender, EventArgs e)
    {
		Navigation.PushAsync(new DefaultCitiesPage(_apiService));
    }
}