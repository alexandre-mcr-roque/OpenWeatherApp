namespace OpenWeatherApp.Extensions
{
    public static class StringExtensions
    {
        /*
         * Code below equivalent to
         * 
         * public static string FirstCharToUpper(this string input)
         * {
         *      switch (input)
         *      {
         *           case null: throw new ArgumentNullException(nameof(input));
         *           case "": return "";
         *           default: return string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1));
         *      }
         * }
         * 
         * Decided to keep this way as a learning opportunity
         */
        public static string FirstCharToUpper(this string input) =>
        input switch
        {
            null => throw new ArgumentNullException(nameof(input)),
            "" => throw new ArgumentException($"{nameof(input)} cannot be empty", nameof(input)),
            _ => string.Concat(input[0].ToString().ToUpper(), input.AsSpan(1))
        };
    }
}
