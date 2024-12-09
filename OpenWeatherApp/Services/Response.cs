using System.Net;

namespace OpenWeatherApp.Services
{
	/// <summary>
	/// Class used to store an HTTP response
	/// <para>It uses a model defined in <typeparamref name="T"/> to store the resulting JSON data from the request</para>
	/// </summary>
	/// <typeparam name="T">The model containing the response data</typeparam>
	public class Response<T> where T: class
	{
		/// <summary>
		/// <c>True</c> if the request was successful
		/// </summary>
		public bool Success { get; set; }

		/// <summary>
		/// The response status code ( <see cref="HttpStatusCode"/> ) or <c>-1</c> if there was an internal error
		/// <para>More information can be found at <a href="https://developer.mozilla.org/en-US/docs/Web/HTTP/Status">developer.mozilla.org</a></para>
		/// </summary>
		public int StatusCode { get; set; }

		/// <summary>
		/// The message given in the response, if it was unsuccessful
		/// </summary>
		public string? Message { get; set; }

		/// <summary>
		/// The resulting data from the request, if it was successful
		/// </summary>
		public T? Data { get; set; }

		/// <summary>
		/// The resulting data from the request serialized, if it was successful
		/// </summary>
		public string? RawData { get; set; }
	}
}
