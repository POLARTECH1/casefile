using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.ViewModels.WindowModal;
using casefile.desktop.Views.WindowModal;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

public class DialogWindowService : IDialogWindowService
{
    private readonly ICreateTemplateDossier _createTemplateDossier;
    private readonly IUpdateTemplateDossier _updateTemplateDossier;
    private readonly IGetTypeDocuments _getTypeDocuments;
    private readonly IGetTemplateDossierForEdit _getTemplateDossierForEdit;

    public DialogWindowService(
        ICreateTemplateDossier createTemplateDossier,
        IUpdateTemplateDossier updateTemplateDossier,
        IGetTypeDocuments getTypeDocuments,
        IGetTemplateDossierForEdit getTemplateDossierForEdit)
    {
        _createTemplateDossier = createTemplateDossier;
        _updateTemplateDossier = updateTemplateDossier;
        _getTypeDocuments = getTypeDocuments;
        _getTemplateDossierForEdit = getTemplateDossierForEdit;
    }

    public async Task<TemplateDossierDto?> ShowCreateTemplateDossierDialog()
    {
        var owner = GetMainWindow();
        var viewModel = new CreateFolderTemplateWindowViewModel(_createTemplateDossier, _getTypeDocuments);
        var dialog = new CreateFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<TemplateDossierDto?>(owner);
    }

    public async Task<TemplateDossierDto?> ShowEditTemplateDossierDialog(Guid id)
    {
        var owner = GetMainWindow();
        var viewModel = new EditFolderTemplateWindowViewModel(_updateTemplateDossier, _getTypeDocuments, _getTemplateDossierForEdit, id);
        var dialog = new EditFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<TemplateDossierDto?>(owner);
    }

    public async Task<bool?> ShowConfirmationDialog(string message, string? confirmButtonText = null,
        string? cancelButtonText = null, string? title = null, bool? result = null,
        Action<bool?>? closeRequested = null)
    {
        var owner = GetMainWindow();
        var viewModel = new ConfirmDialogWindowViewModel(message, confirmButtonText, cancelButtonText, title, result,
            closeRequested);
        var dialog = new ConfirmDialogWindow(viewModel);
        return await dialog.ShowDialog<bool?>(owner);
    }

    private static Window GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime
            && lifetime.MainWindow is not null)
        {
            return lifetime.MainWindow;
        }

        throw new InvalidOperationException("MainWindow est introuvable pour ouvrir la fenêtre modale.");
    }
}