using System;
using casefile.desktop.Models;
using casefile.desktop.ViewModels;
using casefile.desktop.Views.Pages.Clients;
using casefile.desktop.Views.Pages.Templates;
using ReactiveUI;

namespace casefile.desktop.Tools;

/// <summary>
/// Fournit un mécanisme pour localiser les vues associées aux modèles de vue dans l'application.
/// Cette classe est utilisée pour résoudre et retourner l'instance de vue correspondant au modèle de vue donné.
/// Implémente l'interface <c>IViewLocator</c>.
/// </summary>

public class AppViewLocator : ReactiveUI.IViewLocator
{
    public IViewFor? ResolveView<T>(T? viewModel, string? contract = null)
    {
        return viewModel switch
        {
            ClientPageViewModel context => new ClientPageView { DataContext = context },
            TemplatePageViewModel context => new TemplatePageView { DataContext = context },
            _ => throw new ArgumentOutOfRangeException(nameof(viewModel))
        };
    }
}