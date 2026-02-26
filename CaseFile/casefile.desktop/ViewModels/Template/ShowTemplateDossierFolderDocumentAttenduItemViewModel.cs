namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// View model pour afficher un document attendu dans un dossier dans un template
/// </summary>
public class ShowTemplateDossierFolderDocumentAttenduItemViewModel
{
    /// <summary>
    /// Le nom du document attendu
    /// </summary>
    public required string Nom { get; set; }

    /// <summary>
    /// Define si le document attendu est obligatoire
    /// </summary>
    public required bool IsRequired { get; set; }

    /// <summary>
    /// Le type de document attendu
    /// </summary>
    public required string Type { get; set; }
    
    
}