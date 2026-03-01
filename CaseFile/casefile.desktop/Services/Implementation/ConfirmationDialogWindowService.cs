using System.Threading.Tasks;
using casefile.desktop.ViewModels.WindowModal;
using casefile.desktop.Views.WindowModal;

namespace casefile.desktop.Services.Implementation;

public sealed class ConfirmationDialogWindowService
    : DialogWindowServiceBase, IDialogWindowService<ConfirmationDialogRequest, bool?>
{
    public async Task<bool?> Show(ConfirmationDialogRequest request)
    {
        var owner = GetMainWindow();
        var viewModel = new ConfirmDialogWindowViewModel(
            request.Message,
            request.ConfirmButtonText,
            request.CancelButtonText,
            request.Title,
            request.Result,
            request.CloseRequested);
        var dialog = new ConfirmDialogWindow(viewModel);
        return await dialog.ShowDialog<bool?>(owner);
    }
}
