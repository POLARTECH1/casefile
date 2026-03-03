using System;
using System.Windows.Input;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// ViewModel représentant un template de dossier dans un dropdown de sélection paginé.
/// </summary>
public class TemplateDossierDropdownItemViewModel
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Commande exécutée quand l'utilisateur sélectionne cet élément dans le dropdown.
    /// </summary>
    public ICommand? SelectionnerCommand { get; set; }
}
