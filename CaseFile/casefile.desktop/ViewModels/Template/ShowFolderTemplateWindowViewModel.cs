using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Linq;
using casefile.application.DTOs.TemplateDossier;

namespace casefile.desktop.ViewModels.Template;

/// <summary>
/// View model pour afficher un template de dossier
/// </summary>
public partial class ShowFolderTemplateWindowViewModel : ViewModelBase
{
    private readonly IGetTemplateDossierForEdit _getTemplateDossierForEdit;
    private readonly IGetTypeDocuments _getTypeDocuments;
    private readonly Guid _templateId;

    /// <summary>
    /// Le nom du template de dossier
    /// </summary>
    [ObservableProperty] private string _nom = string.Empty;

    /// <summary>
    /// La description du template de dossier.
    /// </summary>
    [NotifyPropertyChangedFor(nameof(HasDescription))]
    [ObservableProperty] private string _description = string.Empty;

    /// <summary>
    /// Message d'erreur de chargement de la fenêtre d'affichage.
    /// </summary>
    [NotifyPropertyChangedFor(nameof(HasLoadingError))]
    [ObservableProperty] private string? _loadingError;

    public ObservableCollection<ShowTemplateDossierFolderItemViewModel> Dossiers { get; } = new();
    public bool HasDescription => string.IsNullOrWhiteSpace(Description) == false;
    public bool HasLoadingError => string.IsNullOrWhiteSpace(LoadingError) == false;

    public ShowFolderTemplateWindowViewModel(
        IGetTemplateDossierForEdit getTemplateDossierForEdit,
        IGetTypeDocuments getTypeDocuments,
        Guid templateId)
    {
        _getTemplateDossierForEdit = getTemplateDossierForEdit;
        _getTypeDocuments = getTypeDocuments;
        _templateId = templateId;
        _ = InitialiserAsync();
    }

    /// <summary>
    /// Charge les données du template et hydrate la vue avec tous les dossiers
    /// et leurs documents attendus.
    /// </summary>
    public async Task InitialiserAsync()
    {
        LoadingError = null;
        Dossiers.Clear();

        var typesResult = await _getTypeDocuments.ExecuteAsync();
        var typeById = typesResult.IsSuccess
            ? typesResult.Value.ToDictionary(
                t => t.Id,
                t => (
                    Nom: t.Nom,
                    Extensions: string.IsNullOrWhiteSpace(t.ExtensionsPermises) ? "-" : t.ExtensionsPermises!
                ))
            : new Dictionary<Guid, (string Nom, string Extensions)>();

        var templateResult = await _getTemplateDossierForEdit.ExecuteAsync(_templateId);
        if (templateResult.IsFailed)
        {
            LoadingError = string.Join(" ", templateResult.Errors.Select(e => e.Message));
            return;
        }

        UpdateTemplateDossierDto template = templateResult.Value;
        Nom = template.Nom;
        Description = template.Description ?? string.Empty;

        foreach (var element in template.CreateTemplateDossierElementDtos.OrderBy(e => e.Ordre))
        {
            var documents = element.CreateDocumentAttendusDto
                .Select(d =>
                {
                    var typeNom = "Type de document inconnu";
                    var extensions = "-";
                    if (d.IdTypeDocument is not null && typeById.TryGetValue(d.IdTypeDocument.Value, out var value))
                    {
                        typeNom = value.Nom;
                        extensions = value.Extensions;
                    }

                    return new ShowTemplateDossierFolderDocumentAttenduItemViewModel
                    {
                        Nom = typeNom,
                        Extensions = extensions,
                        IsRequired = false
                    };
                })
                .ToList();

            Dossiers.Add(new ShowTemplateDossierFolderItemViewModel
            {
                NomDossier = element.Nom,
                DocumentsAttendus = documents
            });
        }
    }
}
