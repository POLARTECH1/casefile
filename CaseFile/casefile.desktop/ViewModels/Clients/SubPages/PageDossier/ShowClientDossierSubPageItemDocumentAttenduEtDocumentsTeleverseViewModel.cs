using System;
using casefile.domain.model;

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
    public String? NomTypeDocumentAttendu { get; set; }

    public bool EstRequis { get; set; }

    /// <summary>
    /// Determine si le document attendu est considéré comme incomplet, c'est-à-dire que le document client est requis et manquant
    /// </summary>
    public bool IsIncomplet { get; set; }
    public bool IsDocumentAttenduPresentDansDossierClient { get; set; }

    public string ExtensionTypeDocumentAttendu { get; set; } = string.Empty;

    /// <summary>
    /// Le document client téléversé correspondant au document attendu, s'il existe. Peut être null si aucun document n'a été téléversé pour ce document attendu.
    /// </summary>
    public DocumentClient? DocumentClient;
}