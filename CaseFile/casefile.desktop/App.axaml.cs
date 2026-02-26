using System;
using System.IO;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using System.Linq;
using Avalonia.Markup.Xaml;
using casefile.application.DTOs.Client.Validation;
using casefile.application.DTOs.CourrielEnvoye.Validation;
using casefile.application.DTOs.DefinitionAttribut.Validation;
using casefile.application.DTOs.DocumentAttendu.Validation;
using casefile.application.DTOs.DocumentClient.Validation;
using casefile.application.DTOs.ProfilEntreprise.Validation;
using casefile.application.DTOs.RegleNommageDocument.Validation;
using casefile.application.DTOs.SchemaClient.Validation;
using casefile.application.DTOs.TemplateCourriel.Validation;
using casefile.application.DTOs.TemplateDossier.Validation;
using casefile.application.DTOs.TemplateDossierElement.Validation;
using casefile.application.DTOs.TypeDocument.Validation;
using casefile.application.DTOs.ValeurAttributClient.Validation;
using casefile.application.Mapping;
using casefile.application.UseCases.Interfaces;
using casefile.application.UseCases.TemplateDossierUseCases;
using casefile.application.UseCases.TypeDocumentUseCases;
using casefile.application.DTOs.TemplateDossier;
using casefile.data.configuration;
using casefile.data.Repositories;
using casefile.data.Repositories.Interface;
using casefile.desktop.Navigation;
using casefile.desktop.Services;
using casefile.desktop.Services.Implementation;
using casefile.desktop.Tools;
using casefile.desktop.ViewModels;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views;
using FluentValidation;
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
#if DEBUG
        this.AttachDeveloperTools();
#endif
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
        //==============================================================================================================
        ConfigureDatabase(services, configuration);
        ConfigureRepositories(services);
        ConfigureViewModel(services);
        ConfigureServices(services);
        ConfigureValidations(services);
        //==============================================================================================================
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
            var appScope = Services.CreateScope();
            var mainWindow = appScope.ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.DataContext = appScope.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            desktop.MainWindow = mainWindow;
            desktop.Exit += (_, _) => appScope.Dispose();
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

    /// <summary>
    /// Configure et enregistre les repo nécessaires pour l'application dans le conteneur de services.
    /// </summary>
    /// <param name="services">La collection de services où les dépôts seront ajoutés.</param>
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

    /// <summary>
    /// Permet de configurer les services qui doivent être utilisés par l'application.
    /// </summary>
    /// <param name="services">La collection de services à configurer.</param>
    private static void ConfigureServices(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => { }, typeof(MapperConfig));
        services.AddScoped<IGetTemplateDossierItems, GetTemplateDossierItems>();
        services.AddScoped<IGetTemplateDossierItem, GetTemplateDossierItem>();
        services.AddScoped<IDeleteTemplateDossier, DeleteTemplateDossier>();
        services.AddScoped<IGetTypeDocuments, GetTypeDocuments>();
        services.AddScoped<ICreateTemplateDossier, CreateTemplateDossier>();
        services.AddScoped<IUpdateTemplateDossier, UpdateTemplateDossier>();
        services.AddScoped<IGetTemplateDossierForEdit, GetTemplateDossierForEdit>();
        services.AddScoped<IDialogWindowService<NoDialogRequest, TemplateDossierDto?>,
            CreateTemplateDossierDialogWindowService>();
        services.AddScoped<IDialogWindowService<Guid, TemplateDossierDto?>,
            EditTemplateDossierDialogWindowService>();
        services.AddScoped<IDialogWindowService<ConfirmationDialogRequest, bool?>,
            ConfirmationDialogWindowService>();
    }

    /// <summary>
    /// Configure les services pour les ViewModels de l'application.
    /// </summary>
    /// <param name="services">Le conteneur de services pour l'injection de dépendances.</param>
    private static void ConfigureViewModel(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>();
        services.AddSingleton<AppScreen>();
        services.AddSingleton<ReactiveUI.IScreen>(sp => sp.GetRequiredService<AppScreen>());
        services.AddSingleton<IAppRouter, AppRouter>();
        services.AddScoped<NavBarViewModel>();
        services.AddScoped<MainWindowViewModel>();
        services.AddScoped<DashboardPageViewModel>();
        services.AddScoped<TemplatePageViewModel>();
        services.AddScoped<ClientPageViewModel>();
        services.AddScoped<SchemaPageViewModel>();
        services.AddScoped<EntreprisePageViewModel>();
    }

    /// <summary>
    /// Configure les règles de validation pour les différents DTOs de l'application.
    /// </summary>
    /// <param name="services">Le conteneur de services à utiliser pour enregistrer les validateurs.</param>
    private static void ConfigureValidations(IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDocumentClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateDocumentClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCourrielEnvoyeDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateCourrielEnvoyeDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTemplateCourrielDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateTemplateCourrielDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTemplateDossierDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateTemplateDossierDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTemplateDossierElementDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateTemplateDossierElementDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateProfilEntrepriseDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateProfilEntrepriseDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateValeurAttributClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateValeurAttributClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateRegleNommageDocumentDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateRegleNommageDocumentDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateSchemaClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateSchemaClientDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDefinitionAttributDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateDefinitionAttributDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDocumentAttenduDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateDocumentAttenduDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateTypeDocumentDtoValidator>();
        services.AddValidatorsFromAssemblyContaining<UpdateTypeDocumentDtoValidator>();
    }
}
