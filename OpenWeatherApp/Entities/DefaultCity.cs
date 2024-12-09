using System.Globalization;

namespace OpenWeatherApp.Entities
{
    /// <summary>
    /// Use the constructor to build the entity
    /// </summary>
    public class DefaultCity : IGeoCity
    {
        public bool IsGeoLocation => false;

        public DefaultCity(string city, string country, string? state = null)
        {
            Name = city;
            Country = country;
            State = state;
        }

        public string Name { get; }

        public string Country { get; }

        public string? State { get; }

        public double Latitude => 0;

        public double Longitude => 0;

        public string CountryName => !string.IsNullOrEmpty(State)
            ? $"{State}, {Country}"
            : Country;

        public string Coordinates => string.Empty;

        public bool ContainsState => !string.IsNullOrEmpty(State);

        public string Serialize()
        {
            return $"0;{Name};{Country};{State}";
        }
    }
}
