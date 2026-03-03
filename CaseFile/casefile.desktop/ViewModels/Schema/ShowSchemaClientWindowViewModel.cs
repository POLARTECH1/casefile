using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.ViewModels;
using CommunityToolkit.Mvvm.ComponentModel;

namespace casefile.desktop.ViewModels.Schema;

public partial class ShowSchemaClientWindowViewModel : ViewModelBase
{
    private readonly IGetSchemaClientForEdit _getSchemaClientForEdit;
    private readonly Guid _schemaId;

    [ObservableProperty] private string _nom = string.Empty;

    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(HasLoadingError))]
    private string? _loadingError;

    public ObservableCollection<ShowSchemaClientPropertyItemViewModel> Proprietes { get; } = new();
    public bool HasLoadingError => string.IsNullOrWhiteSpace(LoadingError) == false;

    public ShowSchemaClientWindowViewModel(
        IGetSchemaClientForEdit getSchemaClientForEdit,
        Guid schemaId)
    {
        _getSchemaClientForEdit = getSchemaClientForEdit;
        _schemaId = schemaId;
        _ = InitialiserAsync();
    }

    public async Task InitialiserAsync()
    {
        LoadingError = null;
        Proprietes.Clear();

        var result = await _getSchemaClientForEdit.ExecuteAsync(_schemaId);
        if (result.IsFailed)
        {
            LoadingError = string.Join(" ", result.Errors.Select(e => e.Message));
            return;
        }

        Nom = result.Value.Nom;
        foreach (var definition in result.Value.CreateDefinitionAttributDtos)
        {
            Proprietes.Add(new ShowSchemaClientPropertyItemViewModel
            {
                Libelle = definition.Libelle,
                Type = definition.Type.ToString(),
                ValeurDefaut = string.IsNullOrWhiteSpace(definition.ValeurDefaut) ? "-" : definition.ValeurDefaut,
                EstRequis = definition.EstRequis
            });
        }
    }
}
