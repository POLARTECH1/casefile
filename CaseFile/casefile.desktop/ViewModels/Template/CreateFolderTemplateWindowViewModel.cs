using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Modèle de vue utilisé pour gérer la création d'un modèle de dossier.
/// Ce modèle fournit les éléments du modèle ainsi que les fonctionnalités
/// nécessaires pour interagir avec les données associées.
/// Il s'agit d'une classe principale dans la gestion des modèles de dossiers.
/// </summary>
public partial class CreateFolderTemplateWindowViewModel : ViewModelBase
{
    /// <summary>
    /// L'ensemble des dossiers et documents attendus qui composent le modèle de dossier à créer.
    /// </summary>
    public ObservableCollection<CreateFolderTemplateElementItemViewModel> FolderTemplateElements { get; } = new();

    /// <summary>
    /// Le nom du modèle de dossier à créer.
    /// </summary>
    [ObservableProperty] private string _nom = string.Empty;

    /// <summary>
    /// La description du modèle de dossier à créer.
    /// </summary>
    [ObservableProperty] private string? _description;
}