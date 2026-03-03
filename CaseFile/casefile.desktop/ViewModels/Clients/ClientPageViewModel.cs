using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Navigation;
using casefile.desktop.Services;
using casefile.desktop.ViewModels;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// Représente le modèle de vue pour la page client dans l'application.
/// Hérite de la classe de base PageViewModelBase pour bénéficier
/// des fonctionnalités communes aux modèles de vue de page.
/// </summary>
public partial class ClientPageViewModel : PageViewModelBase
{
    private readonly IGetClientItems _getClientItems;
    private readonly IDeleteClient _deleteClient;
    private readonly IDialogWindowService<ConfirmationDialogRequest, bool?> _confirmationDialogService;
    private readonly IAppRouter _router;

    public ClientPageViewModel(
        IScreen screen,
        IGetClientItems getClientItems,
        IDeleteClient deleteClient,
        IDialogWindowService<ConfirmationDialogRequest, bool?> confirmationDialogService,
        IAppRouter router) : base(screen)
    {
        _getClientItems = getClientItems;
        _deleteClient = deleteClient;
        _confirmationDialogService = confirmationDialogService;
        _router = router;
        _ = ChargerClientsAsync();
    }

    /// <summary>
    /// ViewModel dédié à l'état des filtres (schémas, documents, recherche texte).
    /// </summary>
    public ClientFiltreViewModel Filtre { get; } = new();

    /// <summary>
    /// La liste des clients à afficher dans la page.
    /// </summary>
    public ObservableCollection<ClientListItemViewModel> ListeClients { get; } = new();

    private async Task ChargerClientsAsync()
    {
        var filtre = Filtre.VersDto();
        var result = await _getClientItems.ExecuteAsync(filtre);
        if (result.IsFailed) return;

        ListeClients.Clear();
        foreach (var client in result.Value)
        {
            if (!Filtre.CorrespondAuFiltreDocuments(client.NombreDocuments)) continue;
            ListeClients.Add(Map(client));
        }
    }

    [RelayCommand]
    private void NaviguerVersCreationClient() => _router.NavigateTo(AppRoute.CreateClient).Subscribe();

    [RelayCommand]
    private async Task AppliquerFiltres()
    {
        await ChargerClientsAsync();
    }

    [RelayCommand]
    private async Task SupprimerClient(Guid clientId)
    {
        var deleteResult = await _confirmationDialogService.Show(new ConfirmationDialogRequest(
            "Etes-vous sur de vouloir supprimer ce client ? Cette action est irreversible."));
        if (deleteResult == true)
        {
            var result = await _deleteClient.ExecuteAsync(clientId);
            if (result.IsFailed) return;

            await ChargerClientsAsync();
        }
    }

    private Task OuvrirClient(Guid clientId)
    {
        _router.NavigateToShowClient(clientId).Subscribe();
        return Task.CompletedTask;
    }

    private ClientListItemViewModel Map(ClientItemDto client)
    {
        return new ClientListItemViewModel
        {
            Id = client.Id,
            NomPrenom = client.NomPrenom,
            Email = client.Email,
            NomSchema = client.NomSchema,
            NombreDocuments = client.NombreDocuments,
            OuvrirClientCommand = new AsyncRelayCommand(() => OuvrirClient(client.Id)),
            SupprimerClientCommand = new AsyncRelayCommand(() => SupprimerClient(client.Id))
        };
    }
}
