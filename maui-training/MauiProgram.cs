﻿using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace maui_training;
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        builder.UseMauiCommunityToolkit(options =>
        {
            options.SetShouldSuppressExceptionsInAnimations(false);
        });


#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
