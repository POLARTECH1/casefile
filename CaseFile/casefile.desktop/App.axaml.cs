using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using casefile.data.configuration;
using casefile.data.Repositories;
using casefile.data.Repositories.Interface;
using casefile.desktop.Tools;
using casefile.desktop.ViewModels;
using casefile.desktop.Views;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
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
        var logDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "CaseFile",
            "logs");
        Directory.CreateDirectory(logDirectory);
        var logFilePath = Path.Combine(logDirectory, "app.log");

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .Enrich.FromLogContext()
            .WriteTo.Console()
            .WriteTo.File(logFilePath, rollingInterval: RollingInterval.Day)
            .CreateLogger();
        var configuration = BuildConfiguration();
        var services = new ServiceCollection();
        services.AddLogging(lb =>
        {
            lb.ClearProviders();
            lb.AddSerilog(Log.Logger, dispose: false);
        });
        ConfigureDatabase(services, configuration);
        ConfigureRepositories(services);
        ConfigureViewModel(services);
        ConfigureServices(services);
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
                DataContext = Services.GetRequiredService<MainWindowViewModel>(),
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


    private static void ConfigureRepositories(IServiceCollection services)
    {
        services.AddScoped<IClientRepository, ClientRepository>();
        services.AddScoped<IDefinitionAttributRepository, DefinitionAttributRepository>();
        services.AddScoped<IDocumentAttenduRepository, DocumentAttenduRepository>();
        services.AddScoped<IDocumentClientRepository, DocumentClientRepository>();
        services.AddScoped<IDossierClientRepository, DossierClientRepository>();
        services.AddScoped<ICourrielEnvoyeRepository, CourrielEnvoyeRepository>();
        services.AddScoped<IProfilEntrepriseRepository, ProfilEntrepriseRepository>();
        services.AddScoped<IRegleNommageDocumentRepository, RegleNommageDocumentRepository>();
        services.AddScoped<ISchemaClientRepository, SchemaClientRepository>();
        services.AddScoped<ITemplateCourrielRepository, TemplateCourrielRepository>();
        services.AddScoped<ITemplateDossierRepository, TemplateDossierRepository>();
        services.AddScoped<ITemplateDossierElementRepository, TemplateDossierElementRepository>();
        services.AddScoped<ITypeDocumentRepository, TypeDocumentRepository>();
        services.AddScoped<IValeurAttributClientRepository, ValeurAttributClientRepository>();
    }

    private static void ConfigureServices(IServiceCollection services)
    {
    }

    private static void ConfigureViewModel(IServiceCollection services)
    {
        services.AddScoped<MainWindowViewModel>();
    }
}