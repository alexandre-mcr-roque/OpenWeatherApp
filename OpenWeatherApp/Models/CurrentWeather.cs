using Newtonsoft.Json;
using System.Net;

namespace OpenWeatherApp.Models
{
	public class CurrentWeather
	{
		/// <inheritdoc cref="Coordinates"/>
		public Coordinates Coord { get; set; } = null!;

		/// <summary>
		/// List of weather conditions
		/// </summary>
		public List<WeatherCondition> Weather { get; set; } = [];

		/// <summary>
		/// API internal parameter
		/// </summary>
		public string Base { get; set; } = null!;

		/// <inheritdoc cref="MainInfo"/>
		public MainInfo Main { get; set; } = null!;

		/// <summary>
		/// The visibility, meters
		/// <br/>The maximum value of the visibility is 10 km (10000)
		/// </summary>
		public int Visibility { get; set; }

		/// <inheritdoc cref="WindInfo"/>
		public WindInfo Wind { get; set; } = null!;

		/// <inheritdoc cref="CloudsInfo"/>
		public CloudsInfo Clouds { get; set; } = null!;

		/// <inheritdoc cref="RainInfo"/>
		public RainInfo? Rain { get; set; }

		/// <inheritdoc cref="SnowInfo"/>
		public SnowInfo? Snow { get; set; }

		/// <summary>
		/// The time of data calculation, unix, UTC
		/// </summary>
		[JsonProperty(PropertyName = "dt")]
		public long DataCalculation { get; set; }

		/// <inheritdoc cref="SysInfo"/>
		public SysInfo? Sys { get; set; }

		/// <summary>
		/// Shift in seconds from UTC
		/// </summary>
		public int Timezone { get; set; }

		/// <summary>
		/// The city ID
		/// </summary>
		public int Id { get; set; }

		/// <summary>
		/// The city name
		/// </summary>
		public string Name { get; set; } = null!;

		/// <summary>
		/// The response status code ( <see cref="HttpStatusCode"/> )
		/// <para>More information can be found at <a href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status">developer.mozilla.org</a></para>
		/// </summary>
		public int Cod { get; set; }


        /// <summary>
        /// A string containing the city and country code if available
        /// </summary>
        public string Location => Sys != null
			? $"{Name}, {Sys.Country}"
			: Name;

		#region Inner Classes
		/// <summary>
		/// The coordinates of the location where data was taken
		/// </summary>
		public class Coordinates
		{
			/// <summary>
			/// Latitude of the location
			/// </summary>
			public double Lat { get; set; }

			/// <summary>
			/// Longitude of the location
			/// </summary>
			public double Lon { get; set; }
		}

		/// <summary>
		/// Information about the weather condition
		/// </summary>
		public class WeatherCondition
		{
			/// <summary>
			/// The weather condition Id
			/// </summary>
			public int Id { get; set; }

			/// <summary>
			/// The group of weather parameters (Rain, Snow, Clouds, ...)
			/// </summary>
			public string Main { get; set; } = null!;

			/// <summary>
			/// The weather condition within the group
			/// <br/>This field has the output changed with the set language
			/// </summary>
			public string Description { get; set; } = null!;

			/// <summary>
			/// The weather icon id
			/// <para><a href="https://openweathermap.org/weather-conditions">More information</a></para>
			/// </summary>
			public string Icon { get; set; } = null!;

			/// <summary>
			/// The weather icon
			/// </summary>
			public string IconSource => $"https://openweathermap.org/img/wn/{Icon}@4x.png";

        }

		/// <summary>
		/// Information about the weather, such as temperature, pressure and humidity
		/// </summary>
		public class MainInfo
		{
			/// <summary>
			/// The temperature of the location
			/// <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>Kelvin</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>Celsius</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>Fahrenheit</description>
			///		</item>
			/// </list>
			/// </summary>
			public double Temp { get; set; }

			/// <summary>
			/// The temperature of the location, accounting for human perception of weather
			/// <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>Kelvin</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>Celsius</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>Fahrenheit</description>
			///		</item>
			/// </list>
			/// </summary>
			public double FeelsLike { get; set; }

			/// <summary>
			/// The atmospheric pressure on the sea level, hPa
			/// </summary>
			public int Pressure { get; set; }

			/// <summary>
			/// The humidity, %
			/// </summary>
			public int Humidity { get; set; }

			/// <summary>
			/// The minimum temperature at the moment
			/// <para>This is the minimal currently observed temperature (within large megalopolises and urban areas)
			/// <br/>For other areas, it will be the same as <see cref="Temp"/></para>
			/// <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>Kelvin</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>Celsius</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>Fahrenheit</description>
			///		</item>
			/// </list>
			/// </summary>
			public double TempMin { get; set; }


			/// <summary>
			/// The maximum temperature at the moment
			/// <para>This is the maximum currently observed temperature (within large megalopolises and urban areas)
			/// <br/>For other areas, it will be the same as <see cref="Temp"/></para>
			/// <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>Kelvin</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>Celsius</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>Fahrenheit</description>
			///		</item>
			/// </list>
			/// </summary>
			public double TempMax { get; set; }

			/// <summary>
			/// The atmospheric pressure on the sea level, hPa
			/// <br/>The value is equal to <see cref="Pressure"/>
			/// </summary>
			public int SeaLevel { get; set; }

			/// <summary>
			/// The atmospheric pressure on the ground level, hPa
			/// </summary>
			public int GrndLevel { get; set; }
		}

		/// <summary>
		/// Information about the wind
		/// </summary>
		public class WindInfo
		{
			/// <summary>
			/// The wind speed
			///  <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>meters/second</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>meters/second</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>miles/hour</description>
			///		</item>
			/// </list>
			/// </summary>
			public double Speed { get; set; }

            /// <summary>
            /// The wind speed described using the <a href="https://en.wikipedia.org/wiki/Beaufort_scale#Modern_scale">Beaufort scale</a>
            /// </summary>
            public string SpeedDescription => Preferences.Get(AppSettings.MeasurementUnit, "metric").Equals("metric")
				? AppSettings.WindSpeedTable[(int)Math.Min(Math.Round(Math.Cbrt(Math.Pow(Speed / 0.836, 2))), 12)]
				: AppSettings.WindSpeedTable[(int)Math.Min(Math.Round(Math.Cbrt(Math.Pow(Speed / 1.870, 2))), 12)];

			/// <summary>
			/// The wind direction in degrees (meteorological)
			/// </summary>
			public int Deg { get; set; }

			/// <summary>
			/// The wind direction described using a 16-point compass rose
			/// </summary>
			public string DegDescription  => AppSettings.WindDirTable[Math.Min((int)Math.Floor((Math.Max(Deg, 0) + 11.25) / 22.5), 16)];

			/// <summary>
			/// The wind gust
			///  <br/>Displays according to the given measurement unit
			/// <para><strong>Available measurement units</strong></para>
			/// <list type="bullet">
			///		<item>
			///			<term>Default</term>
			///			<description>meters/second</description>
			///		</item>
			///		<item>
			///			<term>Metric</term>
			///			<description>meters/second</description>
			///		</item>
			///		<item>
			///			<term>Imperial</term>
			///			<description>miles/hour</description>
			///		</item>
			/// </list>
			/// </summary>
			public double? Gust { get; set; }
		}

		/// <summary>
		/// Information about the clouds
		/// </summary>
		public class CloudsInfo
		{
			/// <summary>
			/// The cloudiness, %
			/// </summary>
			[JsonProperty(PropertyName = "all")]
			public int Cloudiness { get; set; }
		}

		/// <summary>
		/// Information about the rain
		/// </summary>
		public class RainInfo
		{
			/// <summary>
			/// The precipitation, mm/h
			/// </summary>
			[JsonProperty(PropertyName = "1h")]
			public double Precipitation { get; set; }
		}

		/// <summary>
		/// Information about the snow
		/// </summary>
		public class SnowInfo
		{
			/// <summary>
			/// The precipitation, mm/h
			/// </summary>
			[JsonProperty(PropertyName = "1h")]
			public double Precipitation { get; set; }
		}

		/// <summary>
		/// Information about the location like country and sunrise and sunset time
		/// </summary>
		public class SysInfo
		{
			/// <summary>
			/// API internal parameter
			/// </summary>
			public int Type { get; set; }

			/// <summary>
			/// API internal parameter
			/// </summary>
			public int Id { get; set; }

			/// <summary>
			/// API internal parameter
			/// </summary>
			public string? Message { get; set; }

			/// <summary>
			/// The country code
			/// </summary>
			public string Country { get; set; } = null!;

			/// <summary>
			/// Sunrise time, unix, UTC
			/// </summary>
			public long Sunrise { get; set; }

			/// <summary>
			/// Sunset time, unix, UTC
			/// </summary>
			public long Sunset { get; set; }
		}
		#endregion
	}
}
