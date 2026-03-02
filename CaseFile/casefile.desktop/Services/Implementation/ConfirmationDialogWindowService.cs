using System.Threading.Tasks;
using casefile.desktop.ViewModels.WindowModal;
using casefile.desktop.Views.WindowModal;

namespace casefile.desktop.Services.Implementation;

/// <summary>
/// Service responsable de la gestion de la fenêtre de dialogue de confirmation.
/// Cette classe permet d'afficher une fenêtre modale pour confirmer ou annuler une action.
/// Elle implémente l'interface générique <see cref="IDialogWindowService{TRequest, TResult}"/> avec
/// un <see cref="ConfirmationDialogRequest"/> comme paramètre d'entrée et un <see cref="bool?"/> comme résultat.
/// </summary>
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
