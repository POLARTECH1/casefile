using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// Représente le modèle de vue pour la page client dans l'application.
/// Hérite de la classe de base PageViewModelBase pour bénéficier
/// des fonctionnalités communes aux modèles de vue de page.
/// </summary>
public class ClientPageViewModel : PageViewModelBase
{
    private readonly IGetClientItems _getClientItems;

    public ClientPageViewModel(IScreen screen, IGetClientItems getClientItems) : base(screen)
    {
        _getClientItems = getClientItems;
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
            ListeClients.Add(new ClientListItemViewModel
            {
                Id = client.Id,
                NomPrenom = client.NomPrenom,
                Email = client.Email,
                NomSchema = client.NomSchema,
                NombreDocuments = client.NombreDocuments
            });
        }
    }

    /// <summary>
    /// La liste des clients à afficher dans la page. Chaque élément de la liste est représenté par un ClientListItemViewModel, qui contient les informations nécessaires pour afficher les détails d'un client dans la liste. Cette collection est observable, ce qui permet à l'interface utilisateur de se mettre à jour automatiquement lorsque des clients sont ajoutés, modifiés ou supprimés de la liste.
    /// </summary>
    public ObservableCollection<ClientListItemViewModel> ListeClients { get; } = new();
}
