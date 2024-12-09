using System.Globalization;

namespace OpenWeatherApp.Entities
{
    public interface IGeoCity
    {
        public bool IsGeoLocation { get; }

        // For non-geolocation use
        public string Name { get; }
        public string Country { get; }
        public string? State { get; }

        // For geolocation use
        public double Latitude { get; }
        public double Longitude { get; }


        /// <summary>
        /// Country and state (if available) in a single string
        /// </summary>
        public string CountryName { get; }

        /// <summary>
        /// A string containing the coordinates
        /// </summary>
        public string Coordinates { get; }

        /// <summary>
        /// <c>true</c> if the property <see cref="State">State</see> contains a value
        /// </summary>
        public bool ContainsState { get; }

        public string Serialize();

        public static IGeoCity? Deserialize(string? geoCity)
        {
            if (string.IsNullOrEmpty(geoCity)) return null;
            try
            {
                var values = geoCity.Split(";");
                bool isGeoLocation = byte.Parse(values[0]) == 1;

                if (isGeoLocation)
                {
                    return new GeocodeCity(new Models.Geocode
                    {
                        Name = values[1],
                        Country = values[2],
                        State = values[3],
                        Lat = double.Parse(values[4], CultureInfo.InvariantCulture),
                        Lon = double.Parse(values[5], CultureInfo.InvariantCulture),
                    });
                }

                return new DefaultCity(values[1], values[2], values[3]);
            }
            // Something wrong happened
            catch { return null; }
        }
    }
}
