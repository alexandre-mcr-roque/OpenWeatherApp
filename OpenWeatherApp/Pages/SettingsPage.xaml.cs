using Android.Media;
using OpenWeatherApp.Services;
using System.Windows.Input;

namespace OpenWeatherApp.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly List<SettingCollectionItem> _settings;

    public SettingsPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
        _settings = new List<SettingCollectionItem>()
        {
            new SettingCollectionItem
            {
                Text = "Theme",
                ImageSource= "theme",
                Command = new Command(ShowThemeDisplay)
            },
            new SettingCollectionItem
            {
                Text = "Measurement Unit",
                ImageSource= "measure",
                Command = new Command(ShowMeasureUnitDisplay)
            },
            new SettingCollectionItem
            {
                Text = "Sign Out",
                ImageSource= "signout",
                Command = new Command(Signout)
            }
        };
    }


    protected override void OnAppearing()
    {
        base.OnAppearing();
        AppTheme theme = Application.Current!.UserAppTheme;
        CollectionSettings.ItemsSource = _settings;
    }

    private async void ShowThemeDisplay()
    {
        string action = await DisplayActionSheet("Available themes", "Cancel", null, "System default", "Light", "Dark");
        AppTheme selected;

        switch (action)
        {
            case "System default":
                selected = AppTheme.Unspecified; break;
            case "Light":
                selected = AppTheme.Light; break;
            case "Dark":
                selected = AppTheme.Dark; break;
            default: return;
        }
        Application.Current!.UserAppTheme = selected;
        Preferences.Set(AppSettings.AppTheme, (int)selected);
    }

    private async void ShowMeasureUnitDisplay()
    {
        string action = await DisplayActionSheet("Available units", "Cancel", null, "Metric", "Imperial");
        string selected;

        switch (action)
        {
            case "Metric":
                selected = "metric"; break;
            case "Imperial":
                selected = "imperial"; break;
            default: return;
        }
        Preferences.Set(AppSettings.MeasurementUnit, selected);
    }

    private void Signout()
    {
		Preferences.Set(AppSettings.APIKey, null);
		Application.Current!.MainPage = new NavigationPage(new LoginPage(_apiService));
    }

    public class SettingCollectionItem
    {
        public string Text { get; set; } = null!;
        public string ImageSource { get; set; } = null!;
        public ICommand Command { get; set; } = null!;
    }
}