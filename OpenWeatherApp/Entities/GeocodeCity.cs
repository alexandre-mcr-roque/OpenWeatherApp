using OpenWeatherApp.Models;
using System.Globalization;

namespace OpenWeatherApp.Entities
{
    /// <summary>
    /// Use the constructor to build the entity
    /// </summary>
    class GeocodeCity : IGeoCity
    {
        public bool IsGeoLocation => true;

        public GeocodeCity(Geocode geocode)
        {
            Name = geocode.Name;
            Country = geocode.Country;
            State = geocode.State;
            Latitude = geocode.Lat;
            Longitude = geocode.Lon;
        }

        public string Name { get; }

        public string Country { get; }

        public string? State { get; }

        public double Latitude { get; }

        public double Longitude { get; }

        public string CountryName => !string.IsNullOrEmpty(State)
            ? $"{State}, {Country}"
            : Country;

        public string Coordinates => $"[{Latitude.ToString(CultureInfo.InvariantCulture)}, {Longitude.ToString(CultureInfo.InvariantCulture)}]";

        public bool ContainsState => !string.IsNullOrEmpty(State);

        public string Serialize()
        {
            return $"1;{Name};{Country};{State};{Latitude.ToString(CultureInfo.InvariantCulture)};{Longitude.ToString(CultureInfo.InvariantCulture)}";
        }
    }
}
