using System;
using System.Threading.Tasks;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.Views.WindowModal.Schema;

namespace casefile.desktop.Services.Implementation;

public sealed class EditSchemaClientDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<Guid, SchemaClientDto?>
{
    private readonly IUpdateSchemaClient _updateSchemaClient;
    private readonly IGetSchemaClientForEdit _getSchemaClientForEdit;

    public EditSchemaClientDialogWindowService(
        IUpdateSchemaClient updateSchemaClient,
        IGetSchemaClientForEdit getSchemaClientForEdit)
    {
        _updateSchemaClient = updateSchemaClient;
        _getSchemaClientForEdit = getSchemaClientForEdit;
    }

    public async Task<SchemaClientDto?> Show(Guid request)
    {
        var owner = GetMainWindow();
        var viewModel = new EditSchemaClientWindowViewModel(
            _updateSchemaClient,
            _getSchemaClientForEdit,
            request);
        var dialog = new EditSchemaClientWindow(viewModel);
        return await dialog.ShowDialog<SchemaClientDto?>(owner);
    }
}
