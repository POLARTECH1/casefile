using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public class ShowClientDossierSubPageItemViewModel
{
    public string Nom { get; set; } = string.Empty;

    public string NombreDocuments { get; set; } = string.Empty;

    public string NombreDocumentRequis { get; set; } = string.Empty;

    /// <summary>
    /// Determine si le dossier est considere comme complet, c'est-a-dire que tous les documents requis sont presents.
    /// </summary>
    public bool IsDossierComplet { get; set; }

    public IAsyncRelayCommand? AjouterDocumentCommand { get; set; }

    public ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>
        DocumentsAttendusEtTeleverses { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel>();
}
