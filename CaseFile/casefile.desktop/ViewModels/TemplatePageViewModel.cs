using System.Collections.ObjectModel;
using casefile.desktop.ViewModels.Template;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public class TemplatePageViewModel : PageViewModelBase
{
    public TemplatePageViewModel(IScreen screen) : base(screen)
    {
    }

    public bool IsListeTemplateEmpty => ListeTemplate.Count == 0;

    /// <summary>
    /// Propriété représentant une collection observable des modèles de dossiers disponibles.
    /// Chaque élément de la liste est une instance de <see cref="TemplateDossierItemViewModel"/>,
    /// qui contient des informations détaillées telles que le nom, la description,
    /// le nombre de dossiers associés et d'autres métadonnées.
    /// </summary>
    public ObservableCollection<TemplateDossierItemViewModel> ListeTemplate { get; set; }
}