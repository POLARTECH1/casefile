using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using casefile.desktop.ViewModels.Template;
using ReactiveUI;

namespace casefile.desktop.ViewModels;

public class TemplatePageViewModel : PageViewModelBase
{
    public TemplatePageViewModel(IScreen screen) : base(screen)
    {
        ListeTemplate = new ObservableCollection<TemplateDossierItemViewModel>(new List<TemplateDossierItemViewModel>
        {
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Template 1",
                Description = "Description du template 1",
                NombreDeDossiers = 5,
                NombreDocumentsAttendus = 20,
                NombreDeClientsQuiUtilisentCeTemplate = 10
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Template 2",
                Description = "Description du template 2",
                NombreDeDossiers = 3,
                NombreDocumentsAttendus = 15,
                NombreDeClientsQuiUtilisentCeTemplate = 7
            },
            new()
            {
                Id = Guid.NewGuid(),
                Nom = "Template 3",
                Description = "Description du template 3",
                NombreDeDossiers = 8,
                NombreDocumentsAttendus = 30,
                NombreDeClientsQuiUtilisentCeTemplate = 12
            }
        });
    }

    /// <summary>
    /// Propriété représentant une collection observable des modèles de dossiers disponibles.
    /// Chaque élément de la liste est une instance de <see cref="TemplateDossierItemViewModel"/>,
    /// qui contient des informations détaillées telles que le nom, la description,
    /// le nombre de dossiers associés et d'autres métadonnées.
    /// </summary>
    public ObservableCollection<TemplateDossierItemViewModel> ListeTemplate { get; set; }
}