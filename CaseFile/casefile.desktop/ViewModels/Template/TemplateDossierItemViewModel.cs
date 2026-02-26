using System;
using System.Windows.Input;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Template;

public class TemplateDossierItemViewModel
{
    /// <summary>
    /// Identifiant unique du template.
    /// </summary>
    public required Guid Id { get; set; }

    /// <summary>
    /// Nom du template.
    /// </summary>
    public required string Nom { get; set; } = string.Empty;

    /// <summary>
    /// Description optionnelle.
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Nombre total de dossiers liés au template.
    /// </summary>
    public required int NombreDeDossiers { get; set; }

    /// <summary>
    /// Nombre total de documents attendus pour ce template.
    /// </summary>
    public required int NombreDocumentsAttendus { get; set; }

    /// <summary>
    /// Nombre total de clients utilisant ce template.
    /// </summary>
    public required string NombreDeClientsQuiUtilisentCeTemplate { get; set; }

    /// <summary>
    /// Commande utilisée pour supprimer un élément associé au modèle de dossier.
    /// </summary>
    public IAsyncRelayCommand? SupprimerCommand { get; set; }

    /// <summary>
    /// Commande utilisée pour ouvrir la fenêtre de modification du modèle de dossier.
    /// </summary>
    public IAsyncRelayCommand? ModifierCommand { get; set; }
}