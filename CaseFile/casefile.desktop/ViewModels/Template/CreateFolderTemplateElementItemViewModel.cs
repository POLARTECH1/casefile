using System.Collections.ObjectModel;
using casefile.application.DTOs.TypeDocument;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Modèle de vue représentant un dossier à créer dans le template.
/// </summary>
public partial class CreateFolderTemplateElementItemViewModel : ObservableObject
{
    private readonly ObservableCollection<CreateFolderTemplateElementItemViewModel> _parentCollection;
    private readonly ObservableCollection<TypeDocumentDto> _typesDocument;

    public ObservableCollection<CreateFolderTemplateElementItemDocumentAttenduViewModel> DocumentsAttendus { get; } = new();

    public CreateFolderTemplateElementItemViewModel(
        ObservableCollection<CreateFolderTemplateElementItemViewModel> parentCollection,
        ObservableCollection<TypeDocumentDto> typesDocument)
    {
        _parentCollection = parentCollection;
        _typesDocument = typesDocument;
    }

    /// <summary>
    /// Nom du dossier.
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string _nom = string.Empty;

    /// <summary>
    /// Ordre d'affichage dans le template.
    /// </summary>
    [ObservableProperty] private int _ordre;

    /// <summary>
    /// Message d'erreur sur le nom du dossier (null si valide).
    /// </summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string? _nomError;

    public bool HasNomError => NomError != null;

    /// <summary>
    /// Vérifie et valide le champ Nom pour s'assurer qu'il respecte les contraintes définies.
    /// Cette méthode définit un message d'erreur si le champ Nom est vide, contient uniquement
    /// des espaces ou dépasse la longueur maximale autorisée de 150 caractères.
    /// </summary>
    public void ValidateNom()
    {
        if (string.IsNullOrWhiteSpace(Nom))
            NomError = "Le nom du dossier est obligatoire.";
        else if (Nom.Length > 150)
            NomError = "Le nom ne peut pas dépasser 150 caractères.";
        else
            NomError = null;
    }

    /// <summary>
    /// Méthode déclenchée lorsqu'une modification est apportée au champ Nom.
    /// Cette méthode vérifie si une erreur est présente dans le champ Nom et, si c'est le cas,
    /// elle déclenche la validation du champ pour mettre à jour les éventuels messages d'erreur.
    /// </summary>
    /// <param name="value">La nouvelle valeur saisie pour le champ Nom.</param>
    partial void OnNomChanged(string value)
    {
        if (NomError != null)
            ValidateNom();
    }

    /// <summary>
    /// Ajoute un nouveau document attendu à la collection des DocumentsAttendus.
    /// Cette méthode crée une nouvelle instance de CreateFolderTemplateElementItemDocumentAttenduViewModel
    /// et l'ajoute à la liste existante, tout en transmettant la collection parente et les types de documents disponibles.
    /// </summary>
    [RelayCommand]
    private void AjouterDocument()
    {
        DocumentsAttendus.Add(new CreateFolderTemplateElementItemDocumentAttenduViewModel(DocumentsAttendus, _typesDocument));
    }

    /// <summary>
    /// Supprime l'instance actuelle de la collection parente.
    /// Cette méthode est utilisée pour retirer un élément spécifique du modèle de dossier
    /// du template en cours. 
    /// </summary>
    [RelayCommand]
    private void Remove() => _parentCollection.Remove(this);
}
