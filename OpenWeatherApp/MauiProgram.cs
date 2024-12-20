﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using OpenWeatherApp.Services;
using The49.Maui.BottomSheet;

namespace OpenWeatherApp
{
	public static class MauiProgram
	{
		public static MauiApp CreateMauiApp()
		{
			var builder = MauiApp.CreateBuilder();
			builder
				.UseMauiApp<App>()
				.UseMauiCommunityToolkit()
                .UseBottomSheet()
                .ConfigureFonts(fonts =>
				{
					fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
					fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				});

#if DEBUG
			builder.Logging.AddDebug();
#endif

			builder.Services
				.AddHttpClient()
				.AddSingleton<IApiService, ApiService>();

			return builder.Build();
		}
	}
}
