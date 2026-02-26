using System.Threading.Tasks;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Schema;
using casefile.desktop.Views.WindowModal.Schema;

namespace casefile.desktop.Services.Implementation;

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
