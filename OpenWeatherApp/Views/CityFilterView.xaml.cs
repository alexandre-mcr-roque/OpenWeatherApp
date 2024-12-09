using OpenWeatherApp.Entities;
using The49.Maui.BottomSheet;

namespace OpenWeatherApp.Views;

public partial class CityFilterView : BottomSheet
{
	private List<CountryCheckBoxViewModel> _countries;

	public CityFilterView()
	{
		InitializeComponent();

        // Get possible items to check
		_countries = AppSettings.DefaultCities
			.DistinctBy(c => c.Country)
			.Select(c => new CountryCheckBoxViewModel
			{
				Country = c.Country,
				IsChecked = false
			})
			.OrderBy(c => c.Country)
			.ToList();

		CountriesFilter.ItemsSource = _countries;
	}

    public event EventHandler<List<string>>? FilterModified;
	protected virtual void OnFilterModified(List<string> e)
	{
		FilterModified?.Invoke(this, e);
	}

    private void UpdateSelected(object sender, CheckedChangedEventArgs e)
    {
		var selected = new List<string>();
		foreach (var country in _countries)
		{
			if (country.IsChecked) selected.Add(country.Country);
		}
		OnFilterModified(selected);
    }

	private class CountryCheckBoxViewModel
	{
		public string Country { get; set; } = null!;
        public bool IsChecked { get; set; }
	}
}