using System;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Services;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.Views.WindowModal.Schema;

namespace casefile.desktop.Services.Implementation;

public sealed class ShowSchemaClientDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<SchemaClientDialogRequest, object?>
{
    private readonly IGetSchemaClientForEdit _getSchemaClientForEdit;

    public ShowSchemaClientDialogWindowService(IGetSchemaClientForEdit getSchemaClientForEdit)
    {
        _getSchemaClientForEdit = getSchemaClientForEdit;
    }

    public async Task<object?> Show(SchemaClientDialogRequest request)
    {
        var owner = GetMainWindow();
        var viewModel = new ShowSchemaClientWindowViewModel(_getSchemaClientForEdit, request.SchemaClientId);
        var dialog = new ShowSchemaClientWindow(viewModel);
        return await dialog.ShowDialog<object?>(owner);
    }
}
