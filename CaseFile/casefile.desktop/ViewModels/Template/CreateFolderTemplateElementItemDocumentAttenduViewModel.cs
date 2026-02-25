using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Représente le modèle de vue utilisé pour créer un élément de modèle de dossier
/// lié à un document attendu. Ce modèle de vue est utilisé pour gérer les données
/// et fonctionnalités relatives à cet élément au sein de l'application.
/// </summary>
public partial class CreateFolderTemplateElementItemDocumentAttenduViewModel : ObservableObject
{
    public CreateFolderTemplateElementItemDocumentAttenduViewModel()
    {
    }

    /// <summary>
    /// Identifiant du type de document attendu.
    /// Ce champ est utilisé pour spécifier le type de document que cet élément de modèle
    /// de dossier doit attendre ou gérer.
    /// </summary>
    [ObservableProperty] private int _idTypeDocument;

    /// <summary>
    /// Indique si le document attendu est requis.
    /// Ce champ détermine si la présence de ce document est obligatoire dans le cadre
    /// du modèle de dossier ou si elle est facultative.
    /// </summary>
    [ObservableProperty] private bool _estRequis;
}