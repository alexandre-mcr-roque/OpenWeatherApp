using Newtonsoft.Json;
using System.Diagnostics.CodeAnalysis;

namespace OpenWeatherApp.Services
{
    public class ApiService : IApiService
	{
		private readonly HttpClient _httpClient;

		public ApiService(HttpClient httpClient)
		{
			_httpClient = httpClient;
        }

		public async Task<Response<T>> GetRequestAsync<T>(string endpoint) where T : class
		{
			var urlAddress = AppSettings.BaseUrl + endpoint;
			try
			{
				var response = await _httpClient.GetAsync(urlAddress);
				if (response.IsSuccessStatusCode)
				{
					var result = await response.Content.ReadAsStringAsync();
					var data = JsonConvert.DeserializeObject<T>(result, AppSettings.SerializerSettings);
					return new Response<T>
					{
						Success = true,
						StatusCode = (int)response.StatusCode,
						Data = data,
						RawData = result
					};
				}
				else
				{
					var errorResponse = await response.Content.ReadAsStringAsync();
					var errorData = JsonConvert.DeserializeObject<dynamic>(errorResponse);
					var errorMessage = errorData?.message ?? "An unknown error occurred";

					return new Response<T>
					{
						Success = false,
						StatusCode = (int)response.StatusCode,
						Message = errorMessage
					};
				}

			}
			catch
			{
				return new Response<T>
				{
					Success = false,
					StatusCode = -1,
					Message = "An internal error occurred"
				};
			}
		}

		public async Task<Response<T>> GetRequestAsync<T>([StringSyntax(StringSyntaxAttribute.CompositeFormat)] string endpoint, params object?[] args) where T : class
		{
			return await GetRequestAsync<T>(string.Format(endpoint, args));
		}
	}
}
