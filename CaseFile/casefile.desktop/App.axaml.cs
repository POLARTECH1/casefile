using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using casefile.data.configuration;
using casefile.desktop.Tools;
using casefile.desktop.ViewModels;
using casefile.desktop.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace casefile.desktop;

public partial class App : Application
{
    private static IServiceProvider Services { get; set; } = default!;

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        var configuration = BuildConfiguration();
        var services = new ServiceCollection();
        ConfigureDatabase(services, configuration);
        Services = services.BuildServiceProvider();

        using (var scope = Services.CreateScope())
        {
            var db = scope.ServiceProvider.GetRequiredService<CaseFileContext>();
            db.Database.Migrate();
            Seeder.Seed(db);
        }

        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Avoid duplicate validations from both Avalonia and the CommunityToolkit. 
            // More info: https://docs.avaloniaui.net/docs/guides/development-guides/data-validation#manage-validationplugins
            DisableAvaloniaDataAnnotationValidation();
            desktop.MainWindow = new MainWindow
            {
                DataContext = new MainWindowViewModel(),
            };
        }

        base.OnFrameworkInitializationCompleted();
    }

    private void DisableAvaloniaDataAnnotationValidation()
    {
        // Get an array of plugins to remove
        var dataValidationPluginsToRemove =
            BindingPlugins.DataValidators.OfType<DataAnnotationsValidationPlugin>().ToArray();

        // remove each entry found
        foreach (var plugin in dataValidationPluginsToRemove)
        {
            BindingPlugins.DataValidators.Remove(plugin);
        }
    }

    private static IConfiguration BuildConfiguration()
    {
        var env = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT") ?? "Production";

        return new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddJsonFile($"appsettings.{env}.json", optional: true, reloadOnChange: true)
            .AddEnvironmentVariables()
            .Build();
    }

    /// <summary>
    /// Permet de configurer la base de donne qui doit etre utilisee pour l'application
    /// </summary>
    /// <param name="services">Le fournisseur de service </param>
    /// <param name="configuration">La configuration de l'application</param>
    private static void ConfigureDatabase(IServiceCollection services, IConfiguration configuration)
    {
        var dbCfg = configuration.GetSection("Database");
        var provider = dbCfg["Provider"];
        var cs = dbCfg["ConnectionString"];


        if (string.IsNullOrWhiteSpace(provider))
        {
            throw new InvalidOperationException(
                "Database configuration error: 'Database:Provider' is not specified in the application configuration.");
        }

        if (string.IsNullOrWhiteSpace(cs))
        {
            throw new InvalidOperationException(
                "Database configuration error: 'Database:ConnectionString' is not specified in the application configuration.");
        }

        services.AddDbContext<CaseFileContext>(opt =>
        {
            if (string.Equals(provider, "Postgres", StringComparison.OrdinalIgnoreCase))
                opt.UseNpgsql(cs).UseSnakeCaseNamingConvention();
            else
                opt.UseSqlite(cs);
        });
    }

    private static void ConfigureServices(IServiceCollection services)
    {
    }
}