using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using casefile.application.Services;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.WindowModal;
using casefile.desktop.Views.WindowModal;

namespace casefile.desktop.Services.Implementation;

public sealed class UploadClientDocumentDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<UploadClientDocumentDialogRequest, bool?>
{
    private readonly IClientDocumentUploadService _clientDocumentUploadService;
    private readonly IGetTypeDocuments _getTypeDocuments;

    public UploadClientDocumentDialogWindowService(
        IClientDocumentUploadService clientDocumentUploadService,
        IGetTypeDocuments getTypeDocuments)
    {
        _clientDocumentUploadService = clientDocumentUploadService;
        _getTypeDocuments = getTypeDocuments;
    }

    public async Task<bool?> Show(UploadClientDocumentDialogRequest request)
    {
        var owner = GetMainWindow();
        var viewModel = new UploadClientDocumentWindowViewModel(
            _clientDocumentUploadService,
            _getTypeDocuments,
            request.ClientId,
            request.NomDossier,
            () => PickFilePathAsync(owner));
        var dialog = new UploadClientDocumentWindow(viewModel);
        return await dialog.ShowDialog<bool?>(owner);
    }

    private static async Task<string?> PickFilePathAsync(Window owner)
    {
        var files = await owner.StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Choisir un fichier a televerser",
            AllowMultiple = false
        });

        return files.FirstOrDefault()?.Path.LocalPath;
    }
}
