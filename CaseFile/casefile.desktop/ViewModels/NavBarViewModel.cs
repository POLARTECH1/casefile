using System;
using System.Reactive;
using casefile.desktop.Navigation;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

/// <summary>
/// Modèle de vue pour la barre de navigation dans l'application.
/// Permet la gestion des commandes de navigation vers différentes pages.
/// </summary>
public class NavBarViewModel : ReactiveObject, IDisposable
{
    private readonly IAppRouter _appRouter;
    private readonly IDisposable _currentRouteSubscription;
    private AppRoute? _activeRoute;

    /// <summary>
    /// Commande réactive pour naviguer vers le tableau de bord de l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToDashboardPage { get; }

    /// <summary>
    /// Commande réactive pour naviguer vers la page dédiée aux clients dans l'application.
    /// Permet de déclencher une navigation vers la section "Clients" à l'aide du routeur de l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToClientPage { get; }

    /// <summary>
    /// Commande réactive pour naviguer vers la page des schemas dans l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToSchemaPage { get; }

    /// <summary>
    /// Commande réactive pour naviguer vers la page dédiée aux modèles dans l'application.
    /// Permet de déclencher une navigation vers la section "Templates" à l'aide du routeur de l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToTemplatePage { get; }

    /// <summary>
    /// Commande réactive pour naviguer vers la page entreprise dans l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToEntreprisePage { get; }

    public bool IsDashboardActive => _activeRoute == AppRoute.Dashboard;

    public bool IsClientActive => _activeRoute == AppRoute.Clients;

    public bool IsSchemaActive => _activeRoute == AppRoute.Schema;

    public bool IsTemplateActive => _activeRoute == AppRoute.Templates;

    public bool IsEntrepriseActive => _activeRoute == AppRoute.Entreprise;

    public NavBarViewModel(IAppRouter appRouter)
    {
        _appRouter = appRouter;
        SetActiveRoute(_appRouter.CurrentRoute);

        _currentRouteSubscription = _appRouter.CurrentRouteChanged.Subscribe(route => SetActiveRoute(route));

        NavigateToDashboardPage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Dashboard));

        NavigateToClientPage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Clients));

        NavigateToSchemaPage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Schema));

        NavigateToTemplatePage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Templates));

        NavigateToEntreprisePage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Entreprise));
    }

    public void Dispose() => _currentRouteSubscription.Dispose();

    private void SetActiveRoute(AppRoute? route)
    {
        if (_activeRoute == route)
        {
            return;
        }

        this.RaiseAndSetIfChanged(ref _activeRoute, route);
        this.RaisePropertyChanged(nameof(IsDashboardActive));
        this.RaisePropertyChanged(nameof(IsClientActive));
        this.RaisePropertyChanged(nameof(IsSchemaActive));
        this.RaisePropertyChanged(nameof(IsTemplateActive));
        this.RaisePropertyChanged(nameof(IsEntrepriseActive));
    }
}
