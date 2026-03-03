using System;
using ReactiveUI;

namespace casefile.desktop.Navigation;

/// <summary>
/// Interface representant un routeur d'application permettant la navigation entre differentes vues.
/// Prend en charge les operations de navigation et de retour.
/// </summary>
public interface IAppRouter
{
    /// <summary>
    /// Represente la route active actuellement affichee dans l'application.
    /// </summary>
    AppRoute? CurrentRoute { get; }

    /// <summary>
    /// Flux emis a chaque changement de route active.
    /// </summary>
    IObservable<AppRoute> CurrentRouteChanged { get; }

    /// <summary>
    /// Navigue vers une vue specifique selon la route fournie.
    /// </summary>
    /// <param name="route">La route cible representant la vue vers laquelle naviguer.</param>
    /// <returns>Un observable produisant le ViewModel de la vue routee.</returns>
    IObservable<IRoutableViewModel> NavigateTo(AppRoute route);

    /// <summary>
    /// Navigue vers la page d'affichage d'un client.
    /// </summary>
    /// <param name="clientId">Identifiant du client a afficher.</param>
    /// <returns>Un observable produisant le ViewModel de la vue routee.</returns>
    IObservable<IRoutableViewModel> NavigateToShowClient(Guid clientId);

    /// <summary>
    /// Retourne a la vue precedente dans la pile de navigation.
    /// </summary>
    /// <returns>Un observable produisant le ViewModel de la vue precedente.</returns>
    IObservable<IRoutableViewModel> GoBack();
}
