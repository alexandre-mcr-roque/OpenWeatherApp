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
}