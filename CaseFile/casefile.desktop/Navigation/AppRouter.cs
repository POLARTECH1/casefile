using System;
using System.Reactive.Linq;
using System.Reactive.Subjects;
using casefile.desktop.ViewModels;
using casefile.desktop.ViewModels.Clients;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.ViewModels.Template;
using Microsoft.Extensions.DependencyInjection;
using ReactiveUI;

namespace casefile.desktop.Navigation;

/// <summary>
/// Represente un routeur d'application responsable de la navigation entre les differentes vues.
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

    public IObservable<IRoutableViewModel> NavigateTo(AppRoute route)
    {
        DisposeActiveScope();

        _activeRouteScope = _scopeFactory.CreateScope();
        IRoutableViewModel vm = route switch
        {
            AppRoute.Dashboard => _activeRouteScope.ServiceProvider.GetRequiredService<DashboardPageViewModel>(),
            AppRoute.Clients => _activeRouteScope.ServiceProvider.GetRequiredService<ClientPageViewModel>(),
            AppRoute.CreateClient => _activeRouteScope.ServiceProvider.GetRequiredService<CreateClientPageViewModel>(),
            AppRoute.Schema => _activeRouteScope.ServiceProvider.GetRequiredService<SchemaPageViewModel>(),
            AppRoute.Templates => _activeRouteScope.ServiceProvider.GetRequiredService<TemplatePageViewModel>(),
            AppRoute.Entreprise => _activeRouteScope.ServiceProvider.GetRequiredService<EntreprisePageViewModel>(),
            AppRoute.ShowClient => throw new InvalidOperationException("Utiliser NavigateToShowClient(clientId) pour cette route."),
            _ => throw new ArgumentOutOfRangeException(nameof(route), route, "Route non supportee"),
        };

        return _screen.Router.NavigateAndReset.Execute(vm)
            .Do(_ => SetCurrentRoute(route));
    }

    public IObservable<IRoutableViewModel> NavigateToShowClient(Guid clientId)
    {
        DisposeActiveScope();

        _activeRouteScope = _scopeFactory.CreateScope();
        var vm = ActivatorUtilities.CreateInstance<ShowClientPageViewModel>(_activeRouteScope.ServiceProvider, clientId);

        return _screen.Router.NavigateAndReset.Execute(vm)
            .Do(_ => SetCurrentRoute(AppRoute.ShowClient));
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
            CreateClientPageViewModel => AppRoute.CreateClient,
            ShowClientPageViewModel => AppRoute.ShowClient,
            SchemaPageViewModel => AppRoute.Schema,
            TemplatePageViewModel => AppRoute.Templates,
            EntreprisePageViewModel => AppRoute.Entreprise,
            _ => null,
        };
    }
}
