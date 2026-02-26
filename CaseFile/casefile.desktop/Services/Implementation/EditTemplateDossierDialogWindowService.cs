using System;
using System.Threading.Tasks;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

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
