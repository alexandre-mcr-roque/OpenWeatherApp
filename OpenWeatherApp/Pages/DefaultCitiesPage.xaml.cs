using CommunityToolkit.Maui.Alerts;
using OpenWeatherApp.Entities;
using OpenWeatherApp.Services;
using OpenWeatherApp.Views;

namespace OpenWeatherApp.Pages;

public partial class DefaultCitiesPage : ContentPage
{
    private readonly IApiService _apiService;
    private readonly string _apiKey;

    private readonly CityFilterView _filterView;

    public DefaultCitiesPage(IApiService apiService)
	{
		InitializeComponent();
        _apiService = apiService;
        _apiKey = Preferences.Get(AppSettings.APIKey, string.Empty);

        CollectionCities.ItemsSource = AppSettings.DefaultCities.Cast<IGeoCity>();

        _filterView = new();
        _filterView.FilterModified += SetFilter;
    }

    private void ShowFilter(object sender, EventArgs e)
    {
        if (!_filterView.IsLoaded)
            _filterView.ShowAsync();
    }

    private async void SetFilter(object? sender, List<string> e)
    {
        var filteredCities = await Task.Run(() =>
        {
            if (e.Count == 0) return AppSettings.DefaultCities;
            return AppSettings.DefaultCities
                    .Where(c => e.Contains(c.Country));
        });
        CollectionCities.SelectedItem = null;
        BtnSelect.IsEnabled = false;
        CollectionCities.ItemsSource = filteredCities;
    }

    private void CollectionCities_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        BtnSelect.IsEnabled = e.CurrentSelection != null;
    }

    private void SearchMoreOptions(object sender, TappedEventArgs e)
    {
        Navigation.PushAsync(new SearchCitiesPage(_apiService));
    }

    private void Select(object sender, EventArgs e)
    {
        var selected = CollectionCities.SelectedItem as DefaultCity;
        if (selected != null)
        {
            Preferences.Set(AppSettings.FirstTime, false);
            Preferences.Set(AppSettings.SelectedCity, selected.Serialize());

            if (Application.Current!.MainPage is AppShell)
            {
                Navigation.PopToRootAsync();
                return;
            }
            Application.Current!.MainPage = new AppShell(_apiService);
            return;
        }
        Toast.Make("Something wrong happened").Show();
    }
}