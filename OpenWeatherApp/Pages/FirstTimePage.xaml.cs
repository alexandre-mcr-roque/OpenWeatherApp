using OpenWeatherApp.Services;

namespace OpenWeatherApp.Pages;

public partial class FirstTimePage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _apiKey;

    public FirstTimePage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
        _apiKey = Preferences.Get(AppSettings.APIKey, string.Empty);
    }

    private void Continue(object sender, EventArgs e)
    {
        Navigation.PushAsync(new DefaultCitiesPage(_apiService));
    }
}