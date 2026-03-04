using System.Collections.ObjectModel;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public class ShowClientDossierSubPageItemViewModel
{
    public string Nom { get; set; } = string.Empty;

    public string NombreDocuments { get; set; } = string.Empty;

    public string NombreDocumentRequis { get; set; } = string.Empty;

    /// <summary>
    /// Determine si le dossier est considéré comme complet, c'est-à-dire que tous les documents requis sont présents.
    /// </summary>
    public bool IsDossierComplet { get; set; }

    public ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>
        DocumentsAttendusEtTeleverses { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>();
}