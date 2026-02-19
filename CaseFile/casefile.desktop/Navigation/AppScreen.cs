using ReactiveUI;

namespace casefile.desktop.Navigation;

/// <summary>
/// Représente un écran d'application centralisant l'état de navigation.
/// </summary>
/// <remarks>
/// Cette classe implémente l'interface IScreen, agissant en tant que conteneur
/// pour l'objet <see cref="RoutingState"/> utilisé dans la gestion de navigation.
/// Elle est conçue pour être utilisée dans une architecture de navigation réactive.
/// </remarks>
public sealed class AppScreen : IScreen
{
    /// <summary>
    /// Fournit un état global de navigation pour l'application.
    /// </summary>
    /// <remarks>
    /// La propriété Router est une instance de <see cref="RoutingState"/>, un composant central
    /// pour la gestion des itinéraires et des transitions entre différentes vues.
    /// Elle est utilisée pour initier la navigation au sein de l'application.
    /// </remarks>
    public RoutingState Router { get; } = new();
}
