using System.Globalization;

namespace OpenWeatherApp.Models
{
    public class Geocode
    {
        /// <summary>
        /// Name of the found location
        /// </summary>
        public string Name { get; set; } = null!;

        /// <summary>
        /// Dictionary containing the name of the location in different languages
        /// <br/>The key contains the language code and the value contains the translated name
        /// </summary>
        public Dictionary<string, string> LocalNames { get; set; } = null!;

        /// <summary>
        /// Latitude of the location
        /// </summary>
        public double Lat { get; set; }

        /// <summary>
        /// Longitude of the location
        /// </summary>
        public double Lon { get; set; }

        /// <summary>
        /// Country of the found location
        /// </summary>
        public string Country { get; set; } = null!;

        /// <summary>
        /// State of the found location if available
        /// </summary>
        public string? State { get; set; }

        /// <summary>
        /// Country and state (if available) in a single string
        /// </summary>
        public string CountryName => !string.IsNullOrEmpty(State)
            ? $"{State}, {Country}"
            : Country;

        /// <summary>
        /// A string containing the coordinates
        /// </summary>
        public string Coordinates => $"[{Lat.ToString(CultureInfo.InvariantCulture)}, {Lon.ToString(CultureInfo.InvariantCulture)}]";

        /// <summary>
        /// <c>true</c> if the property <see cref="State">State</see> contains a value
        /// </summary>
        public bool ContainsState => !string.IsNullOrEmpty(State);
    }
}
