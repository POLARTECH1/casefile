using System.Collections.Generic;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// View model pour afficher un template de dossier
/// </summary>
public class ShowFolderTemplateWindowViewModel
{
    public List<TemplateDossierItemViewModel> Templates { get; set; } = new List<TemplateDossierItemViewModel>();
}