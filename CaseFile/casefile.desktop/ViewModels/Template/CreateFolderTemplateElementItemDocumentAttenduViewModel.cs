using System.Collections.ObjectModel;
using casefile.application.DTOs.TypeDocument;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Représente le modèle de vue pour un document attendu à créer dans un élément de template de dossier.
/// </summary>
public partial class CreateFolderTemplateElementItemDocumentAttenduViewModel : ObservableObject
{
    /// <summary>
    /// Collection parente contenant les modèles de vue des documents attendus.
    /// Permet de maintenir une référence à l'ensemble des éléments associés.
    /// </summary>
    private readonly ObservableCollection<CreateFolderTemplateElementItemDocumentAttenduViewModel> _parentCollection;

    public ObservableCollection<TypeDocumentDto> TypesDocument { get; }

    public CreateFolderTemplateElementItemDocumentAttenduViewModel(
        ObservableCollection<CreateFolderTemplateElementItemDocumentAttenduViewModel> parentCollection,
        ObservableCollection<TypeDocumentDto> typesDocument)
    {
        _parentCollection = parentCollection;
        TypesDocument = typesDocument;
    }

    /// <summary>
    /// Type de document sélectionné pour ce document attendu.
    /// </summary>
    [ObservableProperty] private TypeDocumentDto? _selectedTypeDocument;

    /// <summary>
    /// Indique si ce document est obligatoire dans le template.
    /// </summary>
    [ObservableProperty] private bool _estRequis;

    [RelayCommand]
    private void Remove() => _parentCollection.Remove(this);
}
