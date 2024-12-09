using CommunityToolkit.Maui.Extensions;
using OpenWeatherApp.Pages;
using OpenWeatherApp.Services;

namespace OpenWeatherApp
{
    public partial class AppShell : Shell
	{
		public AppShell(IApiService apiService)
		{
			InitializeComponent();
			FlyoutImage.Source = GetFlyoutSeasonImage();
            SetPages(apiService);
		}

        private void SetPages(IApiService apiService)
        {
            HomeItem.Content = new HomePage(apiService);
            CityItem.Content = new CityInfoPage(apiService);
            SettingsItem.Content = new SettingsPage(apiService);
            AboutItem.Content = new AboutPage();
        }

        /// <summary>
        /// Gets the source for the flyout header image using <a href="https://imaginekitty.com/599/finding-the-current-season-using-c/">this method</a>
        /// </summary>
        /// <returns>The Image Resource associated with the flyout header according to the current season</returns>
        private string GetFlyoutSeasonImage()
        {
            int doy = DateTime.Now.DayOfYear - Convert.ToInt32(DateTime.IsLeapYear(DateTime.Now.Year) && DateTime.Now.DayOfYear > 59);
            return string.Format("openweather_flyout_{0}",
				(doy < 80 || doy >= 355)
				? "winter"
				: ((doy >= 80 && doy < 172)
					? "spring"
					: ((doy >= 172 && doy < 266)
						? "summer"
						: "fall")));
        }
    }
}
