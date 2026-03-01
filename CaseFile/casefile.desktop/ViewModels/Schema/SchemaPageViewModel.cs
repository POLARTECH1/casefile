using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.DTOs.SchemaClient;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Services;
using casefile.desktop.ViewModels;
using CommunityToolkit.Mvvm.Input;
using ReactiveUI;

namespace casefile.desktop.ViewModels.Schema;

public partial class SchemaPageViewModel : PageViewModelBase
{
    private readonly IGetSchemaClientItems _getSchemaClientItems;
    private readonly IGetSchemaClientItem _getSchemaClientItem;
    private readonly IDialogWindowService<NoDialogRequest, SchemaClientDto?> _createSchemaClientDialogService;
    private readonly IDialogWindowService<Guid, SchemaClientDto?> _editSchemaClientDialogService;
    private readonly IDialogWindowService<SchemaClientDialogRequest, object?> _showSchemaClientDialogService;
    private readonly IDialogWindowService<ConfirmationDialogRequest, bool?> _confirmationDialogService;
    private readonly IDeleteSchemaClient _deleteSchemaClient;

    public SchemaPageViewModel(
        IScreen screen,
        IGetSchemaClientItems getSchemaClientItems,
        IGetSchemaClientItem getSchemaClientItem,
        IDialogWindowService<NoDialogRequest, SchemaClientDto?> createSchemaClientDialogService,
        IDialogWindowService<Guid, SchemaClientDto?> editSchemaClientDialogService,
        IDialogWindowService<SchemaClientDialogRequest, object?> showSchemaClientDialogService,
        IDialogWindowService<ConfirmationDialogRequest, bool?> confirmationDialogService,
        IDeleteSchemaClient deleteSchemaClient) : base(screen)
    {
        _getSchemaClientItems = getSchemaClientItems;
        _getSchemaClientItem = getSchemaClientItem;
        _createSchemaClientDialogService = createSchemaClientDialogService;
        _editSchemaClientDialogService = editSchemaClientDialogService;
        _showSchemaClientDialogService = showSchemaClientDialogService;
        _confirmationDialogService = confirmationDialogService;
        _deleteSchemaClient = deleteSchemaClient;
        ListeSchemas.CollectionChanged += (_, _) => this.RaisePropertyChanged(nameof(IsListeSchemasEmpty));
        _ = ChargerSchemasAsync();
    }

    public ObservableCollection<SchemaClientItemViewModel> ListeSchemas { get; } = new();
    public bool IsListeSchemasEmpty => ListeSchemas.Count == 0;

    private async Task ChargerSchemasAsync()
    {
        var result = await _getSchemaClientItems.ExecuteAsync();
        if (result.IsFailed)
        {
            return;
        }

        ListeSchemas.Clear();
        foreach (var dto in result.Value)
        {
            ListeSchemas.Add(Map(dto));
        }
    }

    [RelayCommand]
    private async Task OuvrirWindowCreateSchemaClient()
    {
        var schema = await _createSchemaClientDialogService.Show(new NoDialogRequest());
        if (schema != null)
        {
            var result = await _getSchemaClientItem.ExecuteAsync(schema.Id);
            if (result.IsFailed)
            {
                return;
            }

            ListeSchemas.Add(Map(result.Value));
        }
    }

    [RelayCommand]
    private async Task ModifierSchemaClient(Guid schemaId)
    {
        var result = await _editSchemaClientDialogService.Show(schemaId);
        if (result != null)
        {
            await ChargerSchemasAsync();
        }
    }

    [RelayCommand]
    private async Task SupprimerSchemaClient(Guid schemaId)
    {
        var deleteResult = await _confirmationDialogService.Show(new ConfirmationDialogRequest(
            "Êtes-vous sûr de vouloir supprimer ce schéma client ? Cette action est irréversible."));
        if (deleteResult == true)
        {
            var result = await _deleteSchemaClient.ExecuteAsync(schemaId);
            if (result.IsFailed)
            {
                return;
            }

            await ChargerSchemasAsync();
        }
    }

    private async Task OuvrirWindowShowSchemaClient(Guid schemaId)
    {
        await _showSchemaClientDialogService.Show(new SchemaClientDialogRequest(schemaId));
    }

    private SchemaClientItemViewModel Map(SchemaClientItemDto dto)
    {
        return new SchemaClientItemViewModel
        {
            Id = dto.Id,
            Nom = dto.Nom,
            DescriptionCourte = "Définissez des propriétés personnalisées selon ce schéma.",
            NombreDeProprietes = dto.NombreDeProprietes,
            NombreDeClientsQuiUtilisentCeSchema =
                dto.NombreDeClientsQuiUtilisentCeSchema +
                $" client{(dto.NombreDeClientsQuiUtilisentCeSchema > 1 ? "s" : "")}",
            OuvrirCommand = new AsyncRelayCommand(() => OuvrirWindowShowSchemaClient(dto.Id)),
            SupprimerCommand = new AsyncRelayCommand(() => SupprimerSchemaClient(dto.Id)),
            ModifierCommand = new AsyncRelayCommand(() => ModifierSchemaClient(dto.Id))
        };
    }
}
