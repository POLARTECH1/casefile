using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels.Template;
using casefile.desktop.Views.WindowModal.Template;

namespace casefile.desktop.Services.Implementation;

public class DialogWindowService : IDialogWindowService
{
    private readonly ICreateTemplateDossier _createTemplateDossier;
    private readonly IGetTypeDocuments _getTypeDocuments;

    public DialogWindowService(ICreateTemplateDossier createTemplateDossier, IGetTypeDocuments getTypeDocuments)
    {
        _createTemplateDossier = createTemplateDossier;
        _getTypeDocuments = getTypeDocuments;
    }

    public async Task<TemplateDossierDto?> ShowCreateTemplateDossierDialog()
    {
        var owner = GetMainWindow();
        var viewModel = new CreateFolderTemplateWindowViewModel(_createTemplateDossier, _getTypeDocuments);
        var dialog = new CreateFolderTemplateWindow(viewModel);
        return await dialog.ShowDialog<TemplateDossierDto?>(owner);
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
