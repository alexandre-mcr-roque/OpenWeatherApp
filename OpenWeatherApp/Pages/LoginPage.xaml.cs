using CommunityToolkit.Maui.Alerts;
using OpenWeatherApp.Models;
using OpenWeatherApp.Services;
using System.Net;

namespace OpenWeatherApp.Pages;

public partial class LoginPage : ContentPage
{
	private readonly IApiService _apiService;

	public LoginPage(IApiService apiService)
	{
		InitializeComponent();
		_apiService = apiService;
	}

	/// <summary>
	/// Pastes the content in the device's clipboard into <see cref="EntryKey">EntryKey</see>
	/// </summary>
	/// <param name="sender">Sender of the event</param>
	/// <param name="e">Event arguments</param>
	private async void PasteClipboard(object sender, EventArgs e)
	{
		EntryKey.Text = await Clipboard.Default.GetTextAsync();
		await Toast.Make("Pasted clipboard").Show();
	}

	/// <summary>
	/// Checks if the key is valid and, if true, stores it into the application's Preferences while setting the main page to the <see cref="AppShell"/> page
	/// </summary>
	/// <param name="sender">Sender of the event</param>
	/// <param name="e">Event arguments</param>
	private async void SubmitKey(object sender, EventArgs e)
	{
		LoadingIndicator.IsRunning = true;
		var key = EntryKey.Text;

		// Verify if the entry has data
		if (string.IsNullOrEmpty(key))
		{
			await Toast.Make("The field is empty").Show();
			return;
		}

		// Make a test API call to verify if the key is valid
		var response = await _apiService.GetRequestAsync<CurrentWeather>($"data/2.5/weather?q=London,uk&appid={key}");

		if (response.Success || response.StatusCode == (int)HttpStatusCode.TooManyRequests)
		{
			// Key is valid (or temporarily locked due to rate limiting)
			Preferences.Set(AppSettings.APIKey, key);
            LoadingIndicator.IsRunning = false;

			bool firstTime = Preferences.Get(AppSettings.FirstTime, true);
			if (firstTime)
			{
				Application.Current!.MainPage = new NavigationPage(new FirstTimePage(_apiService));
			}
			else
			{
				Application.Current!.MainPage = new AppShell(_apiService);
			}
            return;
		}

        LoadingIndicator.IsRunning = false;
        await Toast.Make("Invalid API key").Show();
	}
}