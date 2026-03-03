using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Avalonia.Controls;
using FluentValidation;
using casefile.application.DTOs.DocumentAttendu;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.DTOs.TemplateDossierElement;
using casefile.application.DTOs.TypeDocument;
using casefile.application.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Modèle de vue pour la fenêtre de création d'un template de dossier.
/// </summary>
public partial class CreateFolderTemplateWindowViewModel : ViewModelBase
{
    private readonly ICreateTemplateDossier _createTemplateDossier;

    public event Action<TemplateDossierDto?>? CloseRequested;

    public ObservableCollection<CreateFolderTemplateElementItemViewModel> FolderTemplateElements { get; } = new();
    public ObservableCollection<TypeDocumentDto> TypesDocument { get; } = new();

    /// <summary>Nom du template de dossier.</summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string _nom = string.Empty;

    /// <summary>Description du template de dossier.</summary>
    [ObservableProperty] private string _description = string.Empty;

    /// <summary>Message d'erreur sur le nom (null si valide).</summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string? _nomError;

    /// <summary>Message d'erreur sur la description (null si valide).</summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasDescriptionError))]
    private string? _descriptionError;

    /// <summary>Message d'erreur sur la liste des dossiers (null si valide).</summary>
    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasDossiersError))]
    private string? _dossiersError;

    public bool HasNomError => NomError != null;
    public bool HasDescriptionError => DescriptionError != null;
    public bool HasDossiersError => DossiersError != null;

    public CreateFolderTemplateWindowViewModel(
        ICreateTemplateDossier createTemplateDossier,
        IGetTypeDocuments getTypeDocuments)
    {
        _createTemplateDossier = createTemplateDossier;
        _ = ChargerTypesDocumentAsync(getTypeDocuments);
    }

    private async Task ChargerTypesDocumentAsync(IGetTypeDocuments getTypeDocuments)
    {
        var result = await getTypeDocuments.ExecuteAsync();
        if (result.IsSuccess)
        {
            TypesDocument.Clear();
            foreach (var td in result.Value)
                TypesDocument.Add(td);
        }
    }

    [RelayCommand]
    private void AjouterDossier()
    {
        FolderTemplateElements.Insert(0,
            new CreateFolderTemplateElementItemViewModel(FolderTemplateElements, TypesDocument)
            {
                Ordre = FolderTemplateElements.Count
            });
        DossiersError = null;
    }

    [RelayCommand]
    private async Task CreerTemplate()
    {
        NomError = null;
        DescriptionError = null;
        DossiersError = null;

        var dto = new CreateTemplateDossierDto
        {
            Nom = Nom.Trim(),
            Description = string.IsNullOrWhiteSpace(Description) ? null : Description.Trim(),
            CreateTemplateDossierElementDtos = FolderTemplateElements
                .Select((element, index) => new CreateTemplateDossierElementDto
                {
                    Nom = element.Nom.Trim(),
                    Ordre = index,
                    CreateDocumentAttendusDto = element.DocumentsAttendus
                        .Where(d => d.SelectedTypeDocument != null)
                        .Select(d => new CreateDocumentAttenduDto
                        {
                            IdTypeDocument = d.SelectedTypeDocument!.Id,
                            EstRequis = d.EstRequis
                        }).ToList()
                }).ToList()
        };

        try
        {
            var result = await _createTemplateDossier.ExecuteAsync(dto);
            if (result.IsSuccess)
                CloseRequested?.Invoke(result.Value);
            else
                DossiersError = string.Join(" ", result.Errors.Select(e => e.Message));
        }
        catch (ValidationException ex)
        {
            ApplyValidationErrors(ex);
        }
        catch (Exception)
        {
            DossiersError = "Une erreur inattendue s'est produite. Veuillez réessayer.";
        }
    }

    private void ApplyValidationErrors(ValidationException ex)
    {
        var nomErrors = ex.Errors
            .Where(e => e.PropertyName == nameof(CreateTemplateDossierDto.Nom))
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();
        var descriptionErrors = ex.Errors
            .Where(e => e.PropertyName == nameof(CreateTemplateDossierDto.Description))
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();
        var otherErrors = ex.Errors
            .Where(e => e.PropertyName != nameof(CreateTemplateDossierDto.Nom)
                        && e.PropertyName != nameof(CreateTemplateDossierDto.Description))
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();

        NomError = nomErrors.Count > 0 ? string.Join(" ", nomErrors) : null;
        DescriptionError = descriptionErrors.Count > 0 ? string.Join(" ", descriptionErrors) : null;
        DossiersError = otherErrors.Count > 0 ? string.Join(" ", otherErrors) : null;
    }

    [RelayCommand]
    private void Annuler() => CloseRequested?.Invoke(null);
}