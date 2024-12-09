
namespace OpenWeatherApp.Services
{
    /// <summary>
    /// Service for interacting with APIs via HTTP requests.
    /// </summary>
    public interface IApiService
	{
        //TODO read documentation

        /// <summary>
        /// Sends a GET request to the specified endpoint and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The type into which the response will be deserialized.</typeparam>
        /// <param name="endpoint">The endpoint to which the GET request is sent.</param>
        /// <returns>
        /// A <see cref="Response{T}"/> containing the deserialized data, status code, and success status.
        /// </returns>
		Task<Response<T>> GetRequestAsync<T>(string endpoint) where T : class;

        /// <summary>
        /// Sends a GET request to the specified formatted endpoint and deserializes the response into the specified type.
        /// </summary>
        /// <typeparam name="T">The type into which the response will be deserialized.</typeparam>
        /// <param name="endpoint">The endpoint format string to which the GET request is sent.</param>
        /// <param name="args">The arguments to format the endpoint string.</param>
        /// <returns>
        /// A <see cref="Response{T}"/> containing the deserialized data, status code, and success status.
        /// </returns>
		Task<Response<T>> GetRequestAsync<T>(string endpoint, params object?[] args) where T : class;
	}
}