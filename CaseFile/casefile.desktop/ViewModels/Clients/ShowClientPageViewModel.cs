using System;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Navigation;
using casefile.desktop.ViewModels.Clients.SubPages;
using casefile.desktop.ViewModels.Clients.SubPages.PageDocuments;
using casefile.desktop.ViewModels.Clients.SubPages.PageDossier;
using casefile.desktop.ViewModels.Clients.SubPages.PageEmailHistory;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

public partial class ShowClientPageViewModel : PageViewModelBase
{
    private readonly IAppRouter _router;
    private readonly IGetClientItem _getClientItem;

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

    private bool _isViewDossierActive;
    private bool _isViewDocumentActive;
    private bool _isViewHistoriqueEmailsActive;

    public bool IsViewDossierActive
    {
        get => _isViewDossierActive;
        private set => this.RaiseAndSetIfChanged(ref _isViewDossierActive, value);
    }

    public bool IsViewDocumentActive
    {
        get => _isViewDocumentActive;
        private set => this.RaiseAndSetIfChanged(ref _isViewDocumentActive, value);
    }

    public bool IsViewHistoriqueEmailsActive
    {
        get => _isViewHistoriqueEmailsActive;
        private set => this.RaiseAndSetIfChanged(ref _isViewHistoriqueEmailsActive, value);
    }

    public string NomPrenomClient
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public string Email
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public string Telephone
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    } = string.Empty;

    public string SchemaClient
    {
        get;
        private set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            this.RaisePropertyChanged(nameof(IsClientHasSchema));
        }
    } = string.Empty;

    public bool IsClientHasSchema => string.IsNullOrWhiteSpace(SchemaClient) == false;

    public int NombreDocuments
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public ViewModelBase? ActiveSubPageViewModel
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
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
        ToggleActiveClass();
    }

    [RelayCommand]
    private void AfficherDocuments()
    {
        ActiveSubPageViewModel = new ShowClientDocumentsSubPageViewModel(ClientId);
        ToggleActiveClass();
    }

    [RelayCommand]
    private void AfficherHistoriqueEmails()
    {
        ActiveSubPageViewModel = new ShowClientHistoriqueEmailsSubPageViewModel(ClientId);
        ToggleActiveClass();
    }

    private void ToggleActiveClass()
    {
        IsViewDossierActive = ActiveSubPageViewModel is ShowClientDossiersSubPageViewModel;
        IsViewDocumentActive = ActiveSubPageViewModel is ShowClientDocumentsSubPageViewModel;
        IsViewHistoriqueEmailsActive = ActiveSubPageViewModel is ShowClientHistoriqueEmailsSubPageViewModel;
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
