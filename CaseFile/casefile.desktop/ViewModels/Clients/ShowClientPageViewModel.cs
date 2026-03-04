using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Navigation;
using casefile.desktop.Services;
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
    private readonly IGetClientDossiers _getClientDossiers;
    private readonly IDialogWindowService<UploadClientDocumentDialogRequest, bool?> _uploadClientDocumentDialogService;

    /// <summary>
    /// Semaphore utilisé pour synchroniser et limiter l'accès concurrent
    /// aux opérations liées au chargement des données dans le modèle de vue.
    /// </summary>
    /// <remarks>
    /// Ce sémaphore permet de garantir qu'une seule tâche peut exécuter les opérations
    /// sensibles au chargement des données, telles que le chargement des informations
    /// du client ou des dossiers associés, à un instant donné. Il utilise une capacité
    /// initiale et maximale de 1.
    /// </remarks>
    private readonly SemaphoreSlim _loadingSemaphore = new(1, 1);

    public ShowClientPageViewModel(
        IScreen screen,
        IAppRouter router,
        IGetClientItem getClientItem,
        IGetClientDossiers getClientDossiers,
        IDialogWindowService<UploadClientDocumentDialogRequest, bool?> uploadClientDocumentDialogService,
        Guid clientId) : base(screen)
    {
        _router = router;
        _getClientItem = getClientItem;
        _getClientDossiers = getClientDossiers;
        _uploadClientDocumentDialogService = uploadClientDocumentDialogService;
        ClientId = clientId;

        _ = InitialiserAsync();
    }

    public Guid ClientId { get; }

    /// <summary>
    /// Propriété indiquant si la vue actuelle correspond à la section des dossiers.
    /// </summary>
    /// <remarks>
    /// Cette propriété est mise à jour automatiquement en fonction de l'état de
    /// <c>ActiveSubPageViewModel</c>. Elle est activée lorsque l'objet actif correspond à une instance
    /// de <c>ShowClientDossiersSubPageViewModel</c>.
    /// </remarks>
    /// <value>
    /// <c>true</c> si la vue des dossiers est active ; sinon, <c>false</c>.
    /// </value>
    public bool IsViewDossierActive
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsViewDocumentActive
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public bool IsViewHistoriqueEmailsActive
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
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

    /// <summary>
    /// Propriété représentant le nombre total de documents associés au client.
    /// </summary>
    /// <remarks>
    /// Cette propriété est initialisée lors du chargement des informations client via la méthode
    /// <c>ChargerClientAsync</c>, à partir de la valeur fournie par l'objet <c>ShowClientDto</c>.
    /// </remarks>
    /// <value>
    /// Le nombre total de documents associés au client sous forme d'entier.
    /// </value>
    public int NombreDocuments
    {
        get;
        private set => this.RaiseAndSetIfChanged(ref field, value);
    }

    public string LoadingClientError
    {
        get;
        private set
        {
            this.RaiseAndSetIfChanged(ref field, value);
            this.RaisePropertyChanged(nameof(HasLoadingClientError));
        }
    } = string.Empty;

    public bool HasLoadingClientError => string.IsNullOrWhiteSpace(LoadingClientError) == false;

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

    /// <summary>
    /// Charge et affiche la vue des dossiers associés au client actif.
    /// Cette méthode utilise un sémaphore pour empêcher des chargements multiples simultanés.
    /// Si le modèle de vue actuel n'est pas une instance de ShowClientDossiersSubPageViewModel,
    /// une nouvelle instance est créée et définie comme modèle de vue actif.
    /// Les dossiers correspondants sont ensuite chargés et affichés.
    /// </summary>
    /// <returns>Une tâche représentant l'opération asynchrone.</returns>
    [RelayCommand]
    private async Task AfficherDossiers()
    {
        await _loadingSemaphore.WaitAsync();
        try
        {
            var dossiersViewModel = ActiveSubPageViewModel as ShowClientDossiersSubPageViewModel
                                    ?? new ShowClientDossiersSubPageViewModel(
                                        ClientId,
                                        _getClientDossiers,
                                        _uploadClientDocumentDialogService);

            ActiveSubPageViewModel = dossiersViewModel;
            ToggleActiveClass();
            await dossiersViewModel.ChargerDossiersAsync();
        }
        finally
        {
            _loadingSemaphore.Release();
        }
    }

    /// <summary>
    /// Active la sous-page des documents relatifs au client et met à jour la propriété
    /// <c>ActiveSubPageViewModel</c> avec une nouvelle instance de
    /// <c>ShowClientDocumentsSubPageViewModel</c> correspondant au client identifié.
    /// Cette méthode change également l'état des classes actives grâce à l'appel de
    /// <c>ToggleActiveClass</c>.
    /// </summary>
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

    /// <summary>
    /// Charge les informations du client correspondant à l'identifiant spécifié en appelant le cas d'utilisation associé.
    /// Met à jour les propriétés liées au client dans la vue modèle avec les données obtenues.
    /// En cas d'échec, consigne le message d'erreur lié à la tentative de chargement des informations du client.
    /// </summary>
    /// <returns>Une tâche asynchrone représentant l'opération de chargement des informations du client.</returns>
    private async Task ChargerClientAsync()
    {
        await _loadingSemaphore.WaitAsync();
        try
        {
            LoadingClientError = string.Empty;
            var result = await _getClientItem.ExecuteAsync(ClientId);
            if (result.IsFailed)
            {
                LoadingClientError = string.Join(" ", result.Errors.Select(e => e.Message));
                return;
            }

            var client = result.Value;
            NomPrenomClient = $"{client.Prenom} {client.Nom}".Trim();
            Email = client.Email;
            Telephone = client.Telephone;
            SchemaClient = client.NomSchema;
            NombreDocuments = client.NombreDocuments;
        }
        finally
        {
            _loadingSemaphore.Release();
        }
    }

    /// <summary>
    /// Initialise l'état du ViewModel en chargeant les informations nécessaires pour l'affichage.
    /// Cette méthode exécute les opérations de chargement des données client et des dossiers associés.
    /// </summary>
    /// <returns>Un objet Task représentant l'opération asynchrone en cours.</returns>
    private async Task InitialiserAsync()
    {
        await ChargerClientAsync();
        await AfficherDossiers();
    }
}
