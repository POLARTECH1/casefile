using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.DTOs.TemplateDossier;
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
    private readonly IDeleteTemplateDossier _deleteTemplateDossier;

    public TemplatePageViewModel(IScreen screen, IGetTemplateDossierItems getTemplateDossierItems,
        IDialogWindowService dialogWindowService, IGetTemplateDossierItem getTemplateDossierItem,
        IDeleteTemplateDossier deleteTemplateDossier) : base(screen)
    {
        _getTemplateDossierItems = getTemplateDossierItems;
        _dialogWindowService = dialogWindowService;
        _getTemplateDossierItem = getTemplateDossierItem;
        _deleteTemplateDossier = deleteTemplateDossier;
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
            ListeTemplate.Add(Map(dto));
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

            ListeTemplate.Add(Map(result.Value)
            );
        }
    }

    [RelayCommand]
    private async Task ModifierTemplateDossier(Guid templateId)
    {
        var result = await _dialogWindowService.ShowEditTemplateDossierDialog(templateId);
        if (result != null)
        {
            await ChargerTemplatesAsync();
        }
    }

    [RelayCommand]
    private async Task SupprimerTemplateDossier(Guid templateId)
    {
        var deleteResult = await _dialogWindowService.ShowConfirmationDialog(
            "Êtes-vous sûr de vouloir supprimer ce template de dossier ? Cette action est irréversible.");
        if (deleteResult == true)
        {
            var result = await _deleteTemplateDossier.ExecuteAsync(templateId);
            if (result.IsFailed)
            {
                //TODO: Afficher un message d'erreur'
                return;
            }

            //TODO: Afficher un message de succès
            await ChargerTemplatesAsync();
        }
    }

    #region MappingMethods
    
    private TemplateDossierItemViewModel Map(TemplateDossierItemDto dto)
    {
        return new TemplateDossierItemViewModel
        {
            Id = dto.Id,
            Nom = dto.Nom,
            Description = dto.Description,
            NombreDeDossiers = dto.NombreDeDossiers,
            NombreDocumentsAttendus = dto.NombreDocumentsAttendus,
            NombreDeClientsQuiUtilisentCeTemplate = dto.NombreDeClientsQuiUtilisentCeTemplate +
                                                    $" client{(dto.NombreDeClientsQuiUtilisentCeTemplate > 1 ? "s" : "")}",
            SupprimerCommand = new AsyncRelayCommand(() => SupprimerTemplateDossier(dto.Id)),
            ModifierCommand = new AsyncRelayCommand(() => ModifierTemplateDossier(dto.Id))
        };
    }

    #endregion
}