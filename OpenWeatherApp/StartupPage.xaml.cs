using OpenWeatherApp.Pages;
using OpenWeatherApp.Services;

namespace OpenWeatherApp;

public partial class StartupPage : ContentPage
{
    private readonly IApiService _apiService;

    public StartupPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;

        //Appearing += LoadApp;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadApp();
    }

    private void LoadApp()
    {
        // TODO Check for internet connection
        if (Connectivity.Current.NetworkAccess != NetworkAccess.Internet)
        {
            Loaded += RetryConnection;
            return;
        }

        bool logged = IsLoggedIn();
        if (logged)
        {
            var firstTime = Preferences.Get(AppSettings.FirstTime, true);
            if (firstTime)
            {
                Application.Current!.MainPage = new NavigationPage(new FirstTimePage(_apiService));
                return;
            }
            Application.Current!.MainPage = new AppShell(_apiService);
        }
        else
        {
            Application.Current!.MainPage = new NavigationPage(new LoginPage(_apiService));
        }
    }

    private async void RetryConnection(object? sender, EventArgs e)
    {
        if (await DisplayAlert("Network Error", "Check the internet connection", "Retry", "Exit"))
        {
            Loaded -= RetryConnection;
            LoadApp();
        }
        else
        {
            Application.Current!.Quit();
        }
        return;
    }

    private bool IsLoggedIn()
    {
        var key = Preferences.Get(AppSettings.APIKey, string.Empty);

        if (string.IsNullOrEmpty(key))
        {
            return false;
        }
        return true;
    }
}