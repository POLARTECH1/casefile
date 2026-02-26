using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using casefile.application.DTOs.DocumentAttendu;
using casefile.application.DTOs.TemplateDossier;
using casefile.application.DTOs.TemplateDossierElement;
using casefile.application.DTOs.TypeDocument;
using casefile.application.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// Modèle de vue pour la fenêtre de modification d'un template de dossier.
/// Pré-remplit les champs depuis les données existantes et soumet la mise à jour.
/// </summary>
public partial class EditFolderTemplateWindowViewModel : ViewModelBase
{
    private readonly IUpdateTemplateDossier _updateTemplateDossier;
    private readonly Guid _templateId;

    public event Action<TemplateDossierDto?>? CloseRequested;

    public ObservableCollection<CreateFolderTemplateElementItemViewModel> FolderTemplateElements { get; } = new();
    public ObservableCollection<TypeDocumentDto> TypesDocument { get; } = new();

    /// <summary>Nom du template de dossier.</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string _nom = string.Empty;

    /// <summary>Description du template de dossier.</summary>
    [ObservableProperty] private string _description = string.Empty;

    /// <summary>Message d'erreur sur le nom (null si valide).</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string? _nomError;

    /// <summary>Message d'erreur sur la liste des dossiers (null si valide).</summary>
    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasDossiersError))]
    private string? _dossiersError;

    public bool HasNomError => NomError != null;
    public bool HasDossiersError => DossiersError != null;

    public EditFolderTemplateWindowViewModel(
        IUpdateTemplateDossier updateTemplateDossier,
        IGetTypeDocuments getTypeDocuments,
        IGetTemplateDossierForEdit getTemplateDossierForEdit,
        Guid templateId)
    {
        _updateTemplateDossier = updateTemplateDossier;
        _templateId = templateId;
        _ = InitialiserAsync(getTypeDocuments, getTemplateDossierForEdit);
    }

    private async Task InitialiserAsync(IGetTypeDocuments getTypeDocuments, IGetTemplateDossierForEdit getTemplateDossierForEdit)
    {
        var typesResult = await getTypeDocuments.ExecuteAsync();
        if (typesResult.IsSuccess)
        {
            TypesDocument.Clear();
            foreach (var td in typesResult.Value)
                TypesDocument.Add(td);
        }

        var templateResult = await getTemplateDossierForEdit.ExecuteAsync(_templateId);
        if (templateResult.IsFailed)
            return;

        var dto = templateResult.Value;
        Nom = dto.Nom;
        Description = dto.Description ?? string.Empty;

        FolderTemplateElements.Clear();
        foreach (var e in dto.CreateTemplateDossierElementDtos)
        {
            var elementVm = new CreateFolderTemplateElementItemViewModel(FolderTemplateElements, TypesDocument)
            {
                Nom = e.Nom,
                Ordre = e.Ordre
            };

            foreach (var d in e.CreateDocumentAttendusDto)
            {
                var docVm = new CreateFolderTemplateElementItemDocumentAttenduViewModel(
                    elementVm.DocumentsAttendus, TypesDocument)
                {
                    SelectedTypeDocument = TypesDocument.FirstOrDefault(t => t.Id == d.IdTypeDocument)
                };
                elementVm.DocumentsAttendus.Add(docVm);
            }

            FolderTemplateElements.Add(elementVm);
        }
    }

    partial void OnNomChanged(string value)
    {
        if (NomError != null)
            ValidateNom();
    }

    private bool ValidateNom()
    {
        if (string.IsNullOrWhiteSpace(Nom))
        {
            NomError = "Le nom du template est obligatoire.";
            return false;
        }

        if (Nom.Length > 150)
        {
            NomError = "Le nom ne peut pas dépasser 150 caractères.";
            return false;
        }

        NomError = null;
        return true;
    }

    [RelayCommand]
    private void AjouterDossier()
    {
        FolderTemplateElements.Add(new CreateFolderTemplateElementItemViewModel(FolderTemplateElements, TypesDocument)
        {
            Ordre = FolderTemplateElements.Count
        });
        DossiersError = null;
    }

    [RelayCommand]
    private async Task ModifierTemplate()
    {
        NomError = null;
        DossiersError = null;

        var estValide = ValidateNom();

        if (FolderTemplateElements.Count == 0)
        {
            DossiersError = "Le template doit contenir au moins un dossier.";
            estValide = false;
        }

        foreach (var element in FolderTemplateElements)
        {
            element.ValidateNom();
            if (element.HasNomError) estValide = false;
        }

        if (!estValide) return;

        var dto = new UpdateTemplateDossierDto
        {
            Id = _templateId,
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
                            IdTypeDocument = d.SelectedTypeDocument!.Id
                        }).ToList()
                }).ToList()
        };

        try
        {
            var result = await _updateTemplateDossier.ExecuteAsync(dto);
            if (result.IsSuccess)
                CloseRequested?.Invoke(result.Value);
            else
                DossiersError = string.Join(" ", result.Errors.Select(e => e.Message));
        }
        catch (ValidationException ex)
        {
            DossiersError = string.Join(" ", ex.Errors.Select(e => e.ErrorMessage));
        }
        catch (Exception)
        {
            DossiersError = "Une erreur inattendue s'est produite. Veuillez réessayer.";
        }
    }

    [RelayCommand]
    private void Annuler() => CloseRequested?.Invoke(null);
}
