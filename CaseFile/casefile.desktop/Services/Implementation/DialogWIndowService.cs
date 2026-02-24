using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using casefile.desktop.Views.WindowModal.Template;
using casefile.domain.model;

namespace casefile.desktop.Services.Implementation;

public class DialogWindowService : IDialogWindowService
{
    public async Task<TemplateDossier?> ShowCreateTemplateDossierDialog()
    {
        var owner = GetMainWindow();
        var dialog = new CreateFolderTemplateWindow();
        var result = await dialog.ShowDialog<TemplateDossier>(owner);
        return result;
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
