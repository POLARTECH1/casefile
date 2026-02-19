using System.Reactive;
using casefile.desktop.Navigation;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

/// <summary>
/// Modèle de vue pour la barre de navigation dans l'application.
/// Permet la gestion des commandes de navigation vers différentes pages.
/// </summary>
public class NavBarViewModel
{
    private readonly IAppRouter _appRouter;

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

    public NavBarViewModel(IAppRouter appRouter)
    {
        _appRouter = appRouter;

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
}
