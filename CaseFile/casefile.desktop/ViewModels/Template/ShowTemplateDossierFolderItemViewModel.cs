using System.Collections.Generic;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// View model pour afficher un dossier dans un template
/// </summary>
public class ShowTemplateDossierFolderItemViewModel
{
    /// <summary>
    /// Le nom du dossier
    /// </summary>
    public required string NomDossier { get; set; }

    /// <summary>
    /// Liste des documents attendus pour ce dossier
    /// </summary>
    public required List<ShowTemplateDossierFolderDocumentAttenduItemViewModel> DocumentsAttendus { get; set; }
}