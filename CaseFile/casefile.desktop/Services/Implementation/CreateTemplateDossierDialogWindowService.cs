using System.Threading.Tasks;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

/// <summary>
/// Service permettant d'afficher une fenêtre de dialogue pour la création d'un modèle de dossier.
/// </summary>
/// <remarks>
/// Cette classe implémente le service de dialogue générique en utilisant une requête sans paramètres
/// et en fournissant un modèle de dossier (TemplateDossierDto) en résultat.
/// </remarks>
public sealed class CreateTemplateDossierDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<NoDialogRequest, TemplateDossierDto?>
{
    private readonly ICreateTemplateDossier _createTemplateDossier;
    private readonly IGetTypeDocuments _getTypeDocuments;

    public CreateTemplateDossierDialogWindowService(
        ICreateTemplateDossier createTemplateDossier,
        IGetTypeDocuments getTypeDocuments)
    {
        _createTemplateDossier = createTemplateDossier;
        _getTypeDocuments = getTypeDocuments;
    }

    public async Task<TemplateDossierDto?> Show(NoDialogRequest request)
    {
        var owner = GetMainWindow();
        var viewModel = new CreateFolderTemplateWindowViewModel(_createTemplateDossier, _getTypeDocuments);
        var dialog = new CreateFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<TemplateDossierDto?>(owner);
    }
}
