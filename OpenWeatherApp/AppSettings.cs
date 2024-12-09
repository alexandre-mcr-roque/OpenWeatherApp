using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using OpenWeatherApp.Entities;
using PsychroLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenWeatherApp
{
    /// <summary>
    /// Contains information such as Preferences keys, constants and app information
    /// </summary>
    public static class AppSettings
    {
        #region Keys

        /// <summary>
        /// Preferences key
        /// </summary>
        public static string APIKey => "apiKey";

        /// <summary>
        /// Preferences key
        /// </summary>
        public static string FirstTime => "firstTime";

        /// <summary>
        /// Preferences key
        /// </summary>
        public static string SelectedCity => "selected";

        /// <summary>
        /// Preferences key
        /// </summary>
        public static string AppTheme => "appTheme";

        /// <summary>
        /// Preferences key
        /// </summary>
        public static string MeasurementUnit => "measureUnit";

        #endregion

        #region Constants

        /// <summary>
        /// The JSON serializer settings used
        /// </summary>
        public static JsonSerializerSettings SerializerSettings => new()
        {
            // OpenWeatherApi uses snake_case for it's property naming
            ContractResolver = new DefaultContractResolver { NamingStrategy = new SnakeCaseNamingStrategy() }
        };

        /// <summary>
        /// Base URL for the OpenWeather API
        /// </summary>
        public static string BaseUrl => @"https://api.openweathermap.org/";

        /// <summary>
        /// List of cities to show whenever the user is choosing a new favorite
        /// </summary>
        public static List<DefaultCity> DefaultCities => new List<DefaultCity>()
        {
            new("London", "GB", "England"),
            new("Oxford", "GB", "England"),
            new("New York", "US", "New York"),
            new("San Francisco", "US", "California"),
            new("Lisbon", "PT"),
            new("Porto", "PT"),
            new("Madrid", "ES", "Community of Madrid"),
            new("Seville", "ES", "Andalusia"),
            new("Paris", "FR", "Ile-de-France"),
            new("Bordeaux", "FR", "Nouvelle-Aquitaine"),
            new("Berlin", "DE"),
            new("Frankfurt", "DE", "Hesse"),
            new("Rio de Janeiro", "BR", "Rio de Janeiro"),
            new("São Paulo", "BR", "São Paulo"),
            new("Brisbane", "AU", "Queensland"),
            new("Sydney", "AU", "New South Wales")
        };

        /// <summary>
        /// Array containing the wind speed descriptions in accordance to the <a href="https://en.wikipedia.org/wiki/Beaufort_scale#Modern_scale">Beaufort scale</a>
        /// </summary>
        public static string[] WindSpeedTable = ["calm", "light air", "light breeze", "gentle breeze", "moderate breeze", "fresh breeze", "strong breeze", "near gale", "gale", "severe gale", "storm", "violent storm", "hurricane"];

        /// <summary>
        /// Array containing the wind directions for a 16-point compass rose
        /// </summary>
        public static string[] WindDirTable = ["N", "NNE", "NE", "ENE", "E", "ESE", "SE", "SSE", "S", "SSW", "SW", "WSW", "W", "WNW", "NW", "NNW", "N"];

        /// <summary>
        /// Library to help with math
        /// </summary>
        public static Psychrometrics MetricPsychrometrics => new Psychrometrics(UnitSystem.SI);

        /// <summary>
        /// Library to help with math
        /// </summary>
        public static Psychrometrics ImperialPsychrometrics => new Psychrometrics(UnitSystem.IP);

        #endregion

        #region App Information

        /// <summary>
        /// App version
        /// </summary>
        public static string Version => VersionTracking.CurrentVersion;

        /// <summary>
        /// App author
        /// </summary>
        public static string Author => "Alexandre Roque";

        #endregion
    }
}
