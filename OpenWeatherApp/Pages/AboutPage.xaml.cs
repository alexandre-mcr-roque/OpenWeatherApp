namespace OpenWeatherApp.Pages;

public partial class AboutPage : ContentPage
{
	public AboutPage()
	{
		InitializeComponent();
	}

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LabelVersion.Text = $"Version:  {AppSettings.Version}";
        LabelAuthor.Text = $"Author:  {AppSettings.Author}";
    }

    private async void OpenLink(string url)
    {
        try
        {
            Uri uri = new Uri(url);
            BrowserLaunchOptions options = new BrowserLaunchOptions();

            await Browser.Default.OpenAsync(uri, options);
        }
        catch
        {
            // An unexpected error occurred. No browser may be installed on the device.
        }
    }

    private void OpenLinkedIn(object sender, EventArgs e)
    {
        OpenLink("https://www.linkedin.com/in/alexandre-mcr-roque");
    }

    private void OpenGitHub(object sender, EventArgs e)
    {
        OpenLink("https://github.com/alexandre-mcr-roque");
    }
}