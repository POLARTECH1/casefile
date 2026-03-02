using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
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

    public ClientPageViewModel(
        IScreen screen,
        IGetClientItems getClientItems,
        IDeleteClient deleteClient,
        IDialogWindowService<ConfirmationDialogRequest, bool?> confirmationDialogService) : base(screen)
    {
        _getClientItems = getClientItems;
        _deleteClient = deleteClient;
        _confirmationDialogService = confirmationDialogService;
        _ = ChargerClientsAsync();
    }

    private async Task ChargerClientsAsync()
    {
        var result = await _getClientItems.ExecuteAsync();
        if (result.IsFailed)
        {
            return;
        }

        ListeClients.Clear();
        foreach (var client in result.Value)
        {
            ListeClients.Add(Map(client));
        }
    }

    [RelayCommand]
    private async Task SupprimerClient(Guid clientId)
    {
        var deleteResult = await _confirmationDialogService.Show(new ConfirmationDialogRequest(
            "Etes-vous sur de vouloir supprimer ce client ? Cette action est irreversible."));
        if (deleteResult == true)
        {
            var result = await _deleteClient.ExecuteAsync(clientId);
            if (result.IsFailed)
            {
                return;
            }

            await ChargerClientsAsync();
        }
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
            SupprimerClientCommand = new AsyncRelayCommand(() => SupprimerClient(client.Id))
        };
    }

    /// <summary>
    /// La liste des clients à afficher dans la page. Chaque élément de la liste est représenté par un ClientListItemViewModel, qui contient les informations nécessaires pour afficher les détails d'un client dans la liste. Cette collection est observable, ce qui permet à l'interface utilisateur de se mettre à jour automatiquement lorsque des clients sont ajoutés, modifiés ou supprimés de la liste.
    /// </summary>
    public ObservableCollection<ClientListItemViewModel> ListeClients { get; } = new();
}
