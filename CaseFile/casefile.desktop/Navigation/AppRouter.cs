using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using casefile.desktop.ViewModels;
using casefile.desktop.ViewModels.Template;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace casefile.desktop.Navigation;

/// <summary>
/// Représente un routeur d'application responsable de la navigation entre les différentes vues
/// au sein de l'application. Gère les opérations de navigation, ainsi que la disposition des
/// ressources associées à chaque route.
/// </summary>
public sealed class AppRouter : IAppRouter, IDisposable
{
    private readonly AppScreen _screen;
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly Subject<AppRoute> _currentRouteSubject = new();
    private IServiceScope? _activeRouteScope;
    private AppRoute? _currentRoute;

    public AppRouter(AppScreen screen, IServiceScopeFactory scopeFactory)
    {
        _screen = screen;
        _scopeFactory = scopeFactory;
    }

    public AppRoute? CurrentRoute => _currentRoute;

    public IObservable<AppRoute> CurrentRouteChanged => _currentRouteSubject.AsObservable();

    /// <summary>
    /// Navigue vers une page spécifique en fonction de l'itinéraire fourni.
    /// </summary>
    /// <param name="route">Itinéraire de navigation représentant la page cible.</param>
    /// <returns>Un observable qui publie le modèle de vue récemment chargé.</returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// L'itinéraire spécifié n'est pas pris en charge.
    /// </exception>
    public IObservable<IRoutableViewModel> NavigateTo(AppRoute route)
    {
        DisposeActiveScope();

        _activeRouteScope = _scopeFactory.CreateScope();
        IRoutableViewModel vm = route switch
        {
            AppRoute.Dashboard => _activeRouteScope.ServiceProvider.GetRequiredService<DashboardPageViewModel>(),
            AppRoute.Clients => _activeRouteScope.ServiceProvider.GetRequiredService<ClientPageViewModel>(),
            AppRoute.Schema => _activeRouteScope.ServiceProvider.GetRequiredService<SchemaPageViewModel>(),
            AppRoute.Templates => _activeRouteScope.ServiceProvider.GetRequiredService<TemplatePageViewModel>(),
            AppRoute.Entreprise => _activeRouteScope.ServiceProvider.GetRequiredService<EntreprisePageViewModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(route), route, "Route non supportee"),
        };

        return _screen.Router.NavigateAndReset.Execute(vm)
            .Do(_ => SetCurrentRoute(route));
    }

    public IObservable<IRoutableViewModel> GoBack() =>
        _screen.Router.NavigateBack.Execute()
            .Do(vm =>
            {
                var route = ResolveRoute(vm);
                if (route is not null)
                {
                    SetCurrentRoute(route.Value);
                }
            });

    public void Dispose()
    {
        DisposeActiveScope();
        _currentRouteSubject.Dispose();
    }

    private void DisposeActiveScope()
    {
        _activeRouteScope?.Dispose();
        _activeRouteScope = null;
    }

    private void SetCurrentRoute(AppRoute route)
    {
        _currentRoute = route;
        _currentRouteSubject.OnNext(route);
    }

    private static AppRoute? ResolveRoute(IRoutableViewModel vm)
    {
        return vm switch
        {
            DashboardPageViewModel => AppRoute.Dashboard,
            ClientPageViewModel => AppRoute.Clients,
            SchemaPageViewModel => AppRoute.Schema,
            TemplatePageViewModel => AppRoute.Templates,
            EntreprisePageViewModel => AppRoute.Entreprise,
            _ => null,
        };
    }
}
