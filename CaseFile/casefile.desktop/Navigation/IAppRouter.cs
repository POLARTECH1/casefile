using System;
using ReactiveUI;

namespace casefile.desktop.Navigation;

/// <summary>
/// Interface représentant un routeur d'application permettant la navigation entre différentes vues.
/// Prend en charge les opérations de navigation et de retour.
/// </summary>
public interface IAppRouter
{
    /// <summary>
    /// Représente la route active actuellement affichée dans l'application.
    /// </summary>
    AppRoute? CurrentRoute { get; }

    /// <summary>
    /// Flux émis à chaque changement de route active.
    /// </summary>
    IObservable<AppRoute> CurrentRouteChanged { get; }

    /// <summary>
    /// Navigue vers une vue spécifique selon la route fournie.
    /// </summary>
    /// <param name="route">La route cible représentant la vue vers laquelle naviguer.</param>
    /// <returns>Un observable produisant le ViewModel de la vue routée.</returns>
    IObservable<IRoutableViewModel> NavigateTo(AppRoute route);

    /// <summary>
    /// Retourne à la vue précédente dans la pile de navigation.
    /// </summary>
    /// <returns>Un observable produisant le ViewModel de la vue précédente.</returns>
    IObservable<IRoutableViewModel> GoBack();
}
