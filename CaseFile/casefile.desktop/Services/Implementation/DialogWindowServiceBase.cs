using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;

namespace casefile.desktop.Services.Implementation;

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
