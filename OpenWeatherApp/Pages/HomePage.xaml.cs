using CommunityToolkit.Maui.Alerts;
using OpenWeatherApp.Entities;
using OpenWeatherApp.Extensions;
using OpenWeatherApp.Models;
using OpenWeatherApp.Services;
using System.Net;

namespace OpenWeatherApp.Pages;

public partial class HomePage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _apiKey;

    private string _currentCity = string.Empty;
    private string _currentUnit = string.Empty;

    public HomePage(IApiService apiService)
    {
        InitializeComponent();
        _apiService = apiService;
        _apiKey = Preferences.Get(AppSettings.APIKey, string.Empty);
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        StartWeatherLoad();
    }

    private async void StartWeatherLoad()
    {
        // Get GeoCity
        var city = Preferences.Get(AppSettings.SelectedCity, null);

        // Something wrong happened, act as if it's the first time
        if (string.IsNullOrEmpty(city))
        {
            Preferences.Set(AppSettings.FirstTime, true);
            Application.Current!.MainPage = new NavigationPage(new FirstTimePage(_apiService));
            return;
        }

        // Get Measurement unit
        var unit = Preferences.Get(AppSettings.MeasurementUnit, "metric");

        // Return if the selected city is already loaded with the current measurement unit
        if (city.Equals(_currentCity) && unit.Equals(_currentUnit)) return;
        _currentCity = city;
        _currentUnit = unit;

        LayoutData.IsVisible = false;
        LayoutFailed.IsVisible = false;
        LoadingIndicator.IsRunning = true;

        await LoadWeather(IGeoCity.Deserialize(city)!);

        LoadingIndicator.IsRunning = false;
    }

    private async Task LoadWeather(IGeoCity city)
    {

        // Fetch data from API
        // var response = city.IsGeoLocation
        //     ? await _apiService.GetRequestAsync<CurrentWeather>($"/data/2.5/weather?lat={city.Latitude}&lon={city.Longitude}&appid={_apiKey}&units=metric")
        //     : await _apiService.GetRequestAsync<CurrentWeather>($"/data/2.5/weather?q={city.Name}, {city.CountryName}&appid={_apiKey}");
        //
        // Not using geolocation because, funnily enough, it's "too precise"
        var response = await _apiService.GetRequestAsync<CurrentWeather>($"/data/2.5/weather?q={city.Name}, {city.CountryName}&appid={_apiKey}&units={_currentUnit}");
        if (response.Success)
        {
            DisplayWeather(response.Data!);
            return;
        }

        // Failed to load
        switch (response.StatusCode)
        {
            // API key invalid, moving to login page
            case (int)HttpStatusCode.Unauthorized:
                Application.Current!.MainPage = new NavigationPage(new LoginPage(_apiService));
                return;

            // Problem with the chosen city, force the user to choose a new one
            case (int)HttpStatusCode.NotFound:
                Application.Current!.MainPage = new NavigationPage(new DefaultCitiesPage(_apiService));
                await Toast.Make("Please select a new city").Show();
                return;

            // API key is temporarily locked due to too many requests
            case (int)HttpStatusCode.TooManyRequests:
                await Toast.Make("Your API key surpassed the request limit").Show();
                return;

            // No internet connection
            case -1:
                if (await DisplayAlert("Network Error", "Check the internet connection", "Retry", "Exit"))
                {
                    await LoadWeather(city);
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

    private void DisplayWeather(CurrentWeather weather)
    {
        TitleLocation.Text = weather.Location;
        IconWeather.Source = weather.Weather.First().IconSource;

        LabelTemperature.Text = _currentUnit.Equals("metric")
            ? $"{Math.Round(weather.Main.Temp)}°C"
            : $"{Math.Round(weather.Main.Temp)}°F";

        LabelFeelsLike.Text = _currentUnit.Equals("metric")
            ? $"Feels like {Math.Round(weather.Main.FeelsLike)}°C."
            : $"Feels like {Math.Round(weather.Main.FeelsLike)}°F.";

        LabelWeatherDescription.Text = $"{weather.Weather.First().Description.FirstCharToUpper()}.";
        LabelWindDescription.Text = $"{weather.Wind.SpeedDescription.FirstCharToUpper()}.";
        IconWindDirection.Rotation = weather.Wind.Deg;

        LabelWind.Text = _currentUnit.Equals("metric")
            ? $"{weather.Wind.Speed}m/s  {weather.Wind.DegDescription}"
            : $"{weather.Wind.Speed}mph  {weather.Wind.DegDescription}";

        LabelPressure.Text = $"{weather.Main.Pressure}hPa";
        LabelHumidity.Text = $"Humidity:  {weather.Main.Humidity}%";

        LabelDewPoint.Text = _currentUnit.Equals("metric")
            ? $"Dew Point:  {Math.Round(AppSettings.MetricPsychrometrics.GetTDewPointFromRelHum(weather.Main.Temp, (double)weather.Main.Humidity / 100))}°C"
            : $"Dew Point:  {Math.Round(AppSettings.ImperialPsychrometrics.GetTDewPointFromRelHum(weather.Main.Temp, (double)weather.Main.Humidity / 100))}°F";

        LabelVisibility.Text = $"Visibility:  {(decimal)weather.Visibility / 1000:0.0##}km";

        LayoutData.IsVisible = true;
    }
}