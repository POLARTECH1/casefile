using System;
using System.Collections.Generic;
using casefile.desktop.ViewModels;
using casefile.desktop.ViewModels.Clients;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.Pages.Clients;
using casefile.desktop.Views.Pages.Dashboard;
using casefile.desktop.Views.Pages.Entreprise;
using casefile.desktop.Views.Pages.Schema;
using casefile.desktop.Views.Pages.Templates;
using ReactiveUI;

namespace casefile.desktop.Tools;

/// <summary>
/// Fournit un mécanisme pour localiser les vues associées aux modèles de vue dans l'application.
/// Cette classe est utilisée pour résoudre et retourner l'instance de vue correspondant au modèle de vue donné.
/// Implémente l'interface <c>IViewLocator</c>.
/// </summary>

public class AppViewLocator : ReactiveUI.IViewLocator
{
    private readonly Dictionary<Type, Func<IViewFor>> _mappings = new()
    {
        { typeof(ClientPageViewModel),       () => new ClientPageView() },
        { typeof(CreateClientPageViewModel), () => new CreateClientPageView() },
        { typeof(DashboardPageViewModel),    () => new DashboardPageView() },
        { typeof(EntreprisePageViewModel),   () => new EntreprisePageView() },
        { typeof(SchemaPageViewModel),       () => new SchemaPageView() },
        { typeof(TemplatePageViewModel),     () => new TemplatePageView() },
    };

    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            ClientPageViewModel context       => new ClientPageView { DataContext = context },
            CreateClientPageViewModel context => new CreateClientPageView { DataContext = context },
            DashboardPageViewModel context    => new DashboardPageView { DataContext = context },
            EntreprisePageViewModel context   => new EntreprisePageView { DataContext = context },
            SchemaPageViewModel context       => new SchemaPageView { DataContext = context },
            TemplatePageViewModel context     => new TemplatePageView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }

    public IViewFor<TViewModel>? ResolveView<TViewModel>(string? contract = null) where TViewModel : class
    {
        if (_mappings.TryGetValue(typeof(TViewModel), out var factory))
        {
            return factory() as IViewFor<TViewModel>;
        }
        throw new ArgumentException($"No view found for view model type {typeof(TViewModel).FullName}");
    }

    public IViewFor? ResolveView(object? instance, string? contract = null)
    {
        if (instance == null) return null;
        var viewModelType = instance.GetType();
        if (_mappings.TryGetValue(viewModelType, out var factory))
        {
            var view = factory();
            if (view is { } viewFor)
            {
                viewFor.ViewModel = instance;
                return viewFor;
            }
        }
        throw new ArgumentException($"No view found for view model type {viewModelType.FullName}");
    }
}
