using ReactiveUI;

namespace casefile.desktop.ViewModels;

/// <summary>
/// Représente le modèle de vue pour la page client dans l'application.
/// Hérite de la classe de base PageViewModelBase pour bénéficier
/// des fonctionnalités communes aux modèles de vue de page.
/// </summary>
public class ClientPageViewModel : PageViewModelBase
{
    public ClientPageViewModel(IScreen screen) : base(screen)
    {
    }
}
