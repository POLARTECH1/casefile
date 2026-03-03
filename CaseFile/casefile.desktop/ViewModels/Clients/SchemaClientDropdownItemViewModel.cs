using System;
using System.Collections.Generic;
using System.Windows.Input;
using casefile.application.DTOs.SchemaClient;

namespace casefile.desktop.ViewModels.Clients;

/// <summary>
/// ViewModel représentant un schéma client dans un dropdown de sélection paginé.
/// </summary>
public class SchemaClientDropdownItemViewModel
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
    public List<DefinitionAttributForSelectDto> Definitions { get; set; } = new();

    /// <summary>
    /// Commande exécutée quand l'utilisateur sélectionne cet élément dans le dropdown.
    /// </summary>
    public ICommand? SelectionnerCommand { get; set; }
}
