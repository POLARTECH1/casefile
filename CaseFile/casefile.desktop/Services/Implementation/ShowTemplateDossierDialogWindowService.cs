using System;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

public sealed class ShowTemplateDossierDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<Guid, object?>
{
    private readonly IGetTemplateDossierForEdit _getTemplateDossierForEdit;
    private readonly IGetTypeDocuments _getTypeDocuments;

    public ShowTemplateDossierDialogWindowService(
        IGetTemplateDossierForEdit getTemplateDossierForEdit,
        IGetTypeDocuments getTypeDocuments)
    {
        _getTemplateDossierForEdit = getTemplateDossierForEdit;
        _getTypeDocuments = getTypeDocuments;
    }

    public async Task<object?> Show(Guid request)
    {
        var owner = GetMainWindow();
        var viewModel = new ShowFolderTemplateWindowViewModel(
            _getTemplateDossierForEdit,
            _getTypeDocuments,
            request);
        var dialog = new ShowFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<object?>(owner);
    }
}
