using System.Threading.Tasks;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.Views.WindowModal.Schema;

namespace casefile.desktop.Services.Implementation;

/// <summary>
/// Service responsable de la gestion de la fenêtre de dialogue pour créer un schéma client.
/// </summary>
/// <remarks>
/// Cette classe implémente l'interface <c>IDialogWindowService</c> avec une requête de type <c>NoDialogRequest</c>
/// et un résultat de type <c>SchemaClientDto?</c>. Elle utilise un modèle et une vue spécifiques pour afficher
/// la fenêtre de création d'un schéma client.
/// </remarks>
public sealed class CreateSchemaClientDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<NoDialogRequest, SchemaClientDto?>
{
    private readonly ICreateSchemaClient _createSchemaClient;

    public CreateSchemaClientDialogWindowService(ICreateSchemaClient createSchemaClient)
    {
        _createSchemaClient = createSchemaClient;
    }

    public async Task<SchemaClientDto?> Show(NoDialogRequest request)
    {
        var owner = GetMainWindow();
        var viewModel = new CreateSchemaClientWindowViewModel(_createSchemaClient);
        var dialog = new CreateSchemaClientWindow(viewModel);
        return await dialog.ShowDialog<SchemaClientDto?>(owner);
    }
}
