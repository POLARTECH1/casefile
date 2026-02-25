using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
    private async Task CreerTemplate()
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
                            IdTypeDocument = d.SelectedTypeDocument!.Id
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
