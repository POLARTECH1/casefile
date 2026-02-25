using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Services;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Template;

public partial class TemplatePageViewModel : PageViewModelBase
{
    private readonly IGetTemplateDossierItems _getTemplateDossierItems;
    private readonly IGetTemplateDossierItem _getTemplateDossierItem;
    private readonly IDialogWindowService _dialogWindowService;

    public TemplatePageViewModel(IScreen screen, IGetTemplateDossierItems getTemplateDossierItems,
        IDialogWindowService dialogWindowService, IGetTemplateDossierItem getTemplateDossierItem) : base(screen)
    {
        _getTemplateDossierItems = getTemplateDossierItems;
        _dialogWindowService = dialogWindowService;
        _getTemplateDossierItem = getTemplateDossierItem;
        ListeTemplate.CollectionChanged += (_, _) => this.RaisePropertyChanged(nameof(IsListeTemplateEmpty));
        _ = ChargerTemplatesAsync();
    }

    public bool IsListeTemplateEmpty => ListeTemplate.Count == 0;

    /// <summary>
    /// Propriété représentant une collection observable des modèles de dossiers disponibles.
    /// Chaque élément de la liste est une instance de <see cref="TemplateDossierItemViewModel"/>,
    /// qui contient des informations détaillées telles que le nom, la description,
    /// le nombre de dossiers associés et d'autres métadonnées.
    /// </summary>
    public ObservableCollection<TemplateDossierItemViewModel> ListeTemplate { get; } = new();

    private async Task ChargerTemplatesAsync()
    {
        var result = await _getTemplateDossierItems.ExecuteAsync();
        if (result.IsFailed)
        {
            return;
        }

        ListeTemplate.Clear();
        foreach (var dto in result.Value)
        {
            ListeTemplate.Add(new TemplateDossierItemViewModel
            {
                Id = dto.Id,
                Nom = dto.Nom,
                Description = dto.Description,
                NombreDeDossiers = dto.NombreDeDossiers,
                NombreDocumentsAttendus = dto.NombreDocumentsAttendus,
                NombreDeClientsQuiUtilisentCeTemplate = dto.NombreDeClientsQuiUtilisentCeTemplate
            });
        }
    }

    [RelayCommand]
    private async Task OuvrirWindowCreateTemplateDossier()
    {
        var template = await _dialogWindowService.ShowCreateTemplateDossierDialog();
        if (template != null)
        {
            var result = await _getTemplateDossierItem.ExecuteAsync(template.Id);
            if (result.IsFailed)
            {
                return;
            }

            ListeTemplate.Add(new TemplateDossierItemViewModel
                {
                    Id = result.Value.Id,
                    Nom = result.Value.Nom,
                    Description = result.Value.Description,
                    NombreDeDossiers = result.Value.NombreDeDossiers,
                    NombreDocumentsAttendus = result.Value.NombreDocumentsAttendus,
                    NombreDeClientsQuiUtilisentCeTemplate = result.Value.NombreDeClientsQuiUtilisentCeTemplate
                }
            );
        }
    }
}