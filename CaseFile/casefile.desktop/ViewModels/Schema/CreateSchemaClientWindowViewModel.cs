using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using casefile.application.DTOs.DefinitionAttribut;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;

namespace casefile.desktop.ViewModels.Schema;

public partial class CreateSchemaClientWindowViewModel : ViewModelBase
{
    private readonly ICreateSchemaClient _createSchemaClient;

    public event Action<SchemaClientDto?>? CloseRequested;

    public ObservableCollection<SchemaPropertyItemViewModel> Proprietes { get; } = new();

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string _nom = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasNomError))]
    private string? _nomError;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasProprietesError))]
    private string? _proprietesError;

    public bool HasNomError => NomError != null;
    public bool HasProprietesError => ProprietesError != null;

    public CreateSchemaClientWindowViewModel(ICreateSchemaClient createSchemaClient)
    {
        _createSchemaClient = createSchemaClient;
    }

    [RelayCommand]
    private void AjouterPropriete()
    {
        Proprietes.Add(new SchemaPropertyItemViewModel(Proprietes));
        ProprietesError = null;
    }

    [RelayCommand]
    private async Task CreerSchemaClient()
    {
        NomError = null;
        ProprietesError = null;

        var dto = new CreateSchemaClientDto
        {
            Nom = Nom.Trim(),
            CreateDefinitionAttributDtos = Proprietes.Select((propriete, index) => new CreateDefinitionAttributDto
            {
                Libelle = propriete.Libelle.Trim(),
                Cle = BuildKey(propriete.Libelle, index),
                Type = propriete.Type,
                EstRequis = propriete.EstRequis,
                ValeurDefaut = string.IsNullOrWhiteSpace(propriete.ValeurDefaut)
                    ? null
                    : propriete.ValeurDefaut.Trim()
            }).ToList()
        };

        try
        {
            var result = await _createSchemaClient.ExecuteAsync(dto);
            if (result.IsSuccess)
                CloseRequested?.Invoke(result.Value);
            else
                ProprietesError = string.Join(" ", result.Errors.Select(e => e.Message));
        }
        catch (ValidationException ex)
        {
            ApplyValidationErrors(ex);
        }
        catch (Exception)
        {
            ProprietesError = "Une erreur inattendue s'est produite. Veuillez réessayer.";
        }
    }

    private void ApplyValidationErrors(ValidationException ex)
    {
        var nomErrors = ex.Errors
            .Where(e => e.PropertyName == nameof(CreateSchemaClientDto.Nom))
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();

        var otherErrors = ex.Errors
            .Where(e => e.PropertyName != nameof(CreateSchemaClientDto.Nom))
            .Select(e => e.ErrorMessage)
            .Distinct()
            .ToList();

        NomError = nomErrors.Count > 0 ? string.Join(" ", nomErrors) : null;
        ProprietesError = otherErrors.Count > 0 ? string.Join(" ", otherErrors) : null;
    }

    [RelayCommand]
    private void Annuler() => CloseRequested?.Invoke(null);

    private static string BuildKey(string label, int index)
    {
        var normalized = label.Normalize(NormalizationForm.FormD);
        var builder = new StringBuilder(normalized.Length);

        foreach (var c in normalized)
        {
            var category = CharUnicodeInfo.GetUnicodeCategory(c);
            if (category != UnicodeCategory.NonSpacingMark)
            {
                builder.Append(c);
            }
        }

        var ascii = builder.ToString().Normalize(NormalizationForm.FormC).ToLowerInvariant().Trim();
        ascii = Regex.Replace(ascii, "[^a-z0-9]+", "_").Trim('_');

        if (string.IsNullOrWhiteSpace(ascii))
        {
            ascii = $"propriete_{index + 1}";
        }

        return ascii.Length <= 100 ? ascii : ascii[..100];
    }
}
