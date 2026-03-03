using casefile.desktop.Navigation;
using casefile.domain.model;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

public partial class ShowClientPageViewModel : PageViewModelBase
{
    private readonly IAppRouter _router;

    public ShowClientPageViewModel(IScreen screen, IAppRouter router) : base(screen)
    {
        _router = router;
    }

    #region Commandes de navigation

    public Client Client { get; set; }

    #region Informations primaires du client

    public string NomPrenomCLient => $"{Client.Nom} {Client.Prenom}";
    public bool IsCLientHasSchema => Client.SchemaClient != null;
    public string SchemaClient => Client.SchemaClient?.Nom ?? "";

    #endregion

    #endregion
}