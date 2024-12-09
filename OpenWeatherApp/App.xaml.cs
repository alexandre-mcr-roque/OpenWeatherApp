using OpenWeatherApp.Services;

namespace OpenWeatherApp
{
    public partial class App : Application
	{
        public App(IApiService apiService)
		{
			InitializeComponent();
            UserAppTheme = (AppTheme) Preferences.Get(AppSettings.AppTheme, 0);   // 0 -> AppTheme.Unspecified
            MainPage = new StartupPage(apiService);
        }
    }
}
