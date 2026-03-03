using System;
using System.Threading.Tasks;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

/// <summary>
/// Service permettant de gérer la fenêtre de dialogue pour l'édition des modèles de dossiers.
/// </summary>
/// <remarks>
/// Cette classe fournit une implémentation d'un service de fenêtre de dialogue permettant
/// de charger et d'afficher une fenêtre d'édition pour les modèles de dossiers. Elle prend
/// en charge l'ajout, la modification, et la récupération des informations nécessaires au
/// fonctionnement de cette fenêtre.
/// </remarks>
/// <remarks>
/// Le service repose sur les interfaces suivantes :
/// - <see cref="IUpdateTemplateDossier"/> : utilisée pour appliquer les modifications apportées au modèle de dossier.
/// - <see cref="IGetTypeDocuments"/> : utilisée pour récupérer les types de documents disponibles.
/// - <see cref="IGetTemplateDossierForEdit"/> : utilisée pour charger un modèle de dossier à des fins d'édition.
/// </remarks>
/// <example>
/// Cette classe n'est pas directement instanciable par les utilisateurs finaux.
/// Elle est supposée être injectée et utilisée dans le cadre des services applicatifs.
/// </example>
public sealed class EditTemplateDossierDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<Guid, TemplateDossierDto?>
{
    private readonly IUpdateTemplateDossier _updateTemplateDossier;
    private readonly IGetTypeDocuments _getTypeDocuments;
    private readonly IGetTemplateDossierForEdit _getTemplateDossierForEdit;

    public EditTemplateDossierDialogWindowService(
        IUpdateTemplateDossier updateTemplateDossier,
        IGetTypeDocuments getTypeDocuments,
        IGetTemplateDossierForEdit getTemplateDossierForEdit)
    {
        _updateTemplateDossier = updateTemplateDossier;
        _getTypeDocuments = getTypeDocuments;
        _getTemplateDossierForEdit = getTemplateDossierForEdit;
    }

    public async Task<TemplateDossierDto?> Show(Guid request)
    {
        var owner = GetMainWindow();
        var viewModel = new EditFolderTemplateWindowViewModel(
            _updateTemplateDossier,
            _getTypeDocuments,
            _getTemplateDossierForEdit,
            request);
        var dialog = new EditFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<TemplateDossierDto?>(owner);
    }
}
