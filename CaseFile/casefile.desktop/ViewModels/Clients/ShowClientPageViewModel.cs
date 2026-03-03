using System;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Navigation;
using casefile.desktop.ViewModels.Clients.SubPages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

public partial class ShowClientPageViewModel : PageViewModelBase
{
    private readonly IAppRouter _router;
    private readonly IGetClientItem _getClientItem;

    private string _nomPrenomClient = string.Empty;
    private string _email = string.Empty;
    private string _telephone = string.Empty;
    private string _schemaClient = string.Empty;
    private int _nombreDocuments;
    private ViewModelBase? _activeSubPageViewModel;
    
    public ShowClientPageViewModel(
        IScreen screen,
        IAppRouter router,
        IGetClientItem getClientItem,
        Guid clientId) : base(screen)
    {
        _router = router;
        _getClientItem = getClientItem;
        ClientId = clientId;

        AfficherDossiers();
        _ = ChargerClientAsync();
    }

    public Guid ClientId { get; }

    public bool IsViewDossierActive => ActiveSubPageViewModel is ShowClientDossiersSubPageViewModel;
    public bool IsViewDocumentActive => ActiveSubPageViewModel is ShowClientDocumentsSubPageViewModel;
    public bool IsViewHistoriqueEmailsActive => ActiveSubPageViewModel is ShowClientHistoriqueEmailsSubPageViewModel;
    public string NomPrenomClient
    {
        get => _nomPrenomClient;
        private set => this.RaiseAndSetIfChanged(ref _nomPrenomClient, value);
    }

    public string Email
    {
        get => _email;
        private set => this.RaiseAndSetIfChanged(ref _email, value);
    }

    public string Telephone
    {
        get => _telephone;
        private set => this.RaiseAndSetIfChanged(ref _telephone, value);
    }

    public string SchemaClient
    {
        get => _schemaClient;
        private set
        {
            this.RaiseAndSetIfChanged(ref _schemaClient, value);
            this.RaisePropertyChanged(nameof(IsClientHasSchema));
        }
    }

    public bool IsClientHasSchema => string.IsNullOrWhiteSpace(SchemaClient) == false;

    public int NombreDocuments
    {
        get => _nombreDocuments;
        private set => this.RaiseAndSetIfChanged(ref _nombreDocuments, value);
    }

    public ViewModelBase? ActiveSubPageViewModel
    {
        get => _activeSubPageViewModel;
        private set => this.RaiseAndSetIfChanged(ref _activeSubPageViewModel, value);
    }

    [RelayCommand]
    private void RetourVersListeClients()
    {
        _router.NavigateTo(AppRoute.Clients).Subscribe();
    }

    [RelayCommand]
    private void AfficherDossiers()
    {
        ActiveSubPageViewModel = new ShowClientDossiersSubPageViewModel(ClientId);
    }

    [RelayCommand]
    private void AfficherDocuments()
    {
        ActiveSubPageViewModel = new ShowClientDocumentsSubPageViewModel(ClientId);
    }

    [RelayCommand]
    private void AfficherHistoriqueEmails()
    {
        ActiveSubPageViewModel = new ShowClientHistoriqueEmailsSubPageViewModel(ClientId);
    }

    private async Task ChargerClientAsync()
    {
        var result = await _getClientItem.ExecuteAsync(ClientId);
        if (result.IsFailed)
        {
            return;
        }

        var client = result.Value;
        NomPrenomClient = $"{client.Prenom} {client.Nom}".Trim();
        Email = client.Email;
        Telephone = client.Telephone;
        SchemaClient = client.NomSchema;
        NombreDocuments = client.NombreDocuments;
    }
}
