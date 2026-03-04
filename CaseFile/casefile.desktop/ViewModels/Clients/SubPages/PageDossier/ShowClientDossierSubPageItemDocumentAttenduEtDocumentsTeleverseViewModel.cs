using System;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

/// <summary>
/// View Model représentant les documents attendus associes au document televersé d'un dossier client.
/// </summary>
public class ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel
{
    /// <summary>
    /// Identifiant unique du document attendu.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// Type de document attendu.
    /// </summary>
    public string? NomTypeDocumentAttendu { get; set; }

    public bool EstRequis { get; set; }

    /// <summary>
    /// Determine si le document attendu est considéré comme incomplet, c'est-à-dire que le document client est requis et manquant
    /// </summary>
    public bool IsIncomplet { get; set; }

    public bool IsDocumentAttenduPresentDansDossierClient { get; set; }

    public string ExtensionTypeDocumentAttendu { get; set; } = string.Empty;

    public Guid? DocumentClientId { get; set; }

    public string DocumentClientNomOriginal { get; set; } = string.Empty;

    public string DocumentClientNomStandardise { get; set; } = string.Empty;

    public string DocumentClientCheminPhysique { get; set; } = string.Empty;

    public string DocumentClientExtension { get; set; } = string.Empty;

    public DateTime? DocumentClientAjouteLe { get; set; }
}
