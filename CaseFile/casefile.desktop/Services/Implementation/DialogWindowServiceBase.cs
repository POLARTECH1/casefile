using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace casefile.desktop.Services.Implementation;

/// <summary>
/// Classe de base abstraite pour les services de fenêtres de dialogue modales.
/// Fournit des mécanismes partagés pour gérer les fenêtres liées à l'application.
/// </summary>
/// <remarks>
/// Cette classe inclut une méthode utilitaire pour récupérer la fenêtre principale
/// de l'application, et peut être héritée par diverses implémentations spécifiques
/// à des scénarios de dialogue.
/// </remarks>
/// <exception cref="InvalidOperationException">
/// Déclenchée si la fenêtre principale de l'application ne peut pas être trouvée.
/// </exception>
public abstract class DialogWindowServiceBase
{
    protected static Window GetMainWindow()
    {
        if (Application.Current?.ApplicationLifetime is IClassicDesktopStyleApplicationLifetime lifetime
            && lifetime.MainWindow is not null)
        {
            return lifetime.MainWindow;
        }

        throw new InvalidOperationException("MainWindow est introuvable pour ouvrir la fenêtre modale.");
    }
}
