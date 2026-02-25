using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Modèle de vue représentant un élément de modèle de dossier à créer.
/// Ce modèle de vue est utilisé pour gérer et refléter les données associées
/// à un élément spécifique d'un modèle de dossier.
/// </summary>
public partial class CreateFolderTemplateElementItemViewModel : ObservableObject
{
    /// <summary>
    /// Les documents attendus associés à cet élément de modèle de dossier.
    /// </summary>
    public ObservableCollection<CreateFolderTemplateElementItemDocumentAttenduViewModel> DocumentsAttendus { get; } =
        new();

    public CreateFolderTemplateElementItemViewModel()
    {
    }

    /// <summary>
    /// Nom de l'élément de modèle de dossier.
    /// </summary>
    [ObservableProperty] private string _nom = string.Empty;

    /// <summary>
    /// Ordre de l'élément dans le modèle de dossier.
    /// Représente la position ou la séquence à laquelle cet élément
    /// doit apparaître dans le modèle.
    /// </summary>
    [ObservableProperty] private int _ordre;
}