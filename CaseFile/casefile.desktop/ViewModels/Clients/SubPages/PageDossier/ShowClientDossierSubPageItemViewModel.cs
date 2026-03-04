using System.Collections.ObjectModel;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public class ShowClientDossierSubPageItemViewModel
{
    public string Nom { get; set; } = string.Empty;

    public int NombreDocuments { get; set; }

    public int NombreDocumentRequis { get; set; }

    /// <summary>
    /// Determine si le dossier est considéré comme complet, c'est-à-dire que tous les documents requis sont présents.
    /// </summary>
    public bool IsDossierComplet { get; set; }

    public ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>
        DocumentsAttendusEtTeleverses { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>();
}