using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using casefile.application.DTOs.DocumentClient;
using casefile.application.UseCases.Interfaces;
using casefile.application.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.WindowModal;

public partial class UploadClientDocumentWindowViewModel : ViewModelBase
{
    private readonly IClientDocumentUploadService _clientDocumentUploadService;
    private readonly IGetTypeDocuments _getTypeDocuments;
    private readonly Func<Task<string?>> _pickFileAsync;

    public UploadClientDocumentWindowViewModel(
        IClientDocumentUploadService clientDocumentUploadService,
        IGetTypeDocuments getTypeDocuments,
        Guid clientId,
        string nomDossier,
        Func<Task<string?>> pickFileAsync)
    {
        _clientDocumentUploadService = clientDocumentUploadService;
        _getTypeDocuments = getTypeDocuments;
        _pickFileAsync = pickFileAsync;
        ClientId = clientId;
        NomDossier = nomDossier;
        _ = ChargerTypesDocumentAsync();
    }

    public event Action<bool?>? CloseRequested;

    public Guid ClientId { get; }
    public string NomDossier { get; }
    public ObservableCollection<UploadTypeDocumentItemViewModel> TypesDocument { get; } = new();

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasError))]
    private string? _errorMessage;

    [ObservableProperty] [NotifyPropertyChangedFor(nameof(HasSelectedFile))]
    private string _cheminFichierSource = string.Empty;

    [ObservableProperty]
    private UploadTypeDocumentItemViewModel? _typeDocumentSelectionne;

    public bool HasError => string.IsNullOrWhiteSpace(ErrorMessage) == false;
    public bool HasSelectedFile => string.IsNullOrWhiteSpace(CheminFichierSource) == false;

    [RelayCommand]
    private async Task ChoisirFichier()
    {
        ErrorMessage = null;
        var chemin = await _pickFileAsync();
        if (string.IsNullOrWhiteSpace(chemin))
        {
            return;
        }

        CheminFichierSource = chemin;
    }

    [RelayCommand]
    private async Task Televerser()
    {
        try
        {
            ErrorMessage = null;
            if (string.IsNullOrWhiteSpace(CheminFichierSource))
            {
                ErrorMessage = "Veuillez choisir un fichier avant de televerser.";
                return;
            }

            if (TypeDocumentSelectionne is null)
            {
                ErrorMessage = "Veuillez selectionner un type de document.";
                return;
            }

            var result = await _clientDocumentUploadService.ExecuteAsync(new UploadClientDocumentRequestDto
            {
                ClientId = ClientId,
                NomDossier = NomDossier,
                CheminFichierSource = CheminFichierSource,
                TypeDocumentId = TypeDocumentSelectionne?.Id
            });

            if (result.IsFailed)
            {
                ErrorMessage = string.Join(" ", result.Errors.Select(e => e.Message));
                return;
            }

            CloseRequested?.Invoke(true);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Erreur inattendue pendant le televersement: {ex.Message}";
        }
    }

    [RelayCommand]
    private void Annuler()
    {
        CloseRequested?.Invoke(false);
    }

    private async Task ChargerTypesDocumentAsync()
    {
        var result = await _getTypeDocuments.ExecuteAsync();
        if (result.IsFailed)
        {
            return;
        }

        TypesDocument.Clear();
        foreach (var type in result.Value.OrderBy(t => t.Nom))
        {
            TypesDocument.Add(new UploadTypeDocumentItemViewModel
            {
                Id = type.Id,
                Nom = type.Nom
            });
        }
    }
}

public class UploadTypeDocumentItemViewModel
{
    public Guid Id { get; set; }
    public string Nom { get; set; } = string.Empty;
}
