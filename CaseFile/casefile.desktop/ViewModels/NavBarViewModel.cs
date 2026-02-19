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
    /// Commande réactive pour naviguer vers la page dédiée aux clients dans l'application.
    /// Permet de déclencher une navigation vers la section "Clients" à l'aide du routeur de l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToClientPage { get; }

    /// <summary>
    /// Commande réactive pour naviguer vers la page dédiée aux modèles dans l'application.
    /// Permet de déclencher une navigation vers la section "Templates" à l'aide du routeur de l'application.
    /// </summary>
    public ReactiveCommand<Unit, IRoutableViewModel> NavigateToTemplatePage { get; }

    public NavBarViewModel(IAppRouter appRouter)
    {
        _appRouter = appRouter;

        NavigateToClientPage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Clients));

        NavigateToTemplatePage =
            ReactiveCommand.CreateFromObservable(() =>
                _appRouter.NavigateTo(AppRoute.Templates));
    }
}
