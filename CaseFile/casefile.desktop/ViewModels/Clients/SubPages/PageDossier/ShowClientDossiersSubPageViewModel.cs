using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.desktop.Services;
using CommunityToolkit.Mvvm.Input;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public sealed class ShowClientDossiersSubPageViewModel : ViewModelBase
{
    private readonly IGetClientDossiers _getClientDossiers;
    private readonly IDialogWindowService<UploadClientDocumentDialogRequest, bool?> _uploadClientDocumentDialogService;

    private readonly SemaphoreSlim _loadingSemaphore = new(1, 1);

    public ShowClientDossiersSubPageViewModel(
        Guid clientId,
        IGetClientDossiers getClientDossiers,
        IDialogWindowService<UploadClientDocumentDialogRequest, bool?> uploadClientDocumentDialogService)
    {
        ClientId = clientId;
        _getClientDossiers = getClientDossiers;
        _uploadClientDocumentDialogService = uploadClientDocumentDialogService;
    }

    public Guid ClientId { get; }

    public ObservableCollection<ShowClientDossierSubPageItemViewModel> Dossiers { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemViewModel>();

    public async Task ChargerDossiersAsync()
    {
        await _loadingSemaphore.WaitAsync();
        try
        {
            var result = await _getClientDossiers.ExecuteAsync(ClientId);
            if (result.IsFailed)
            {
                return;
            }

            Dossiers.Clear();
            foreach (var dossier in result.Value)
            {
                Dossiers.Add(MapDossier(dossier));
            }
        }
        finally
        {
            _loadingSemaphore.Release();
        }
    }

    private ShowClientDossierSubPageItemViewModel MapDossier(ShowClientDossierDto dossier)
    {
        var item = new ShowClientDossierSubPageItemViewModel
        {
            Nom = dossier.Nom,
            NombreDocuments = dossier.NombreDocuments + " document(s)",
            NombreDocumentRequis = dossier.NombreDocumentRequis + " requis",
            IsDossierComplet = dossier.IsDossierComplet,
            AjouterDocumentCommand = new AsyncRelayCommand(() => AjouterDocumentAuDossierAsync(dossier.Nom))
        };

        foreach (var document in dossier.DocumentsAttendusEtTeleverses)
        {
            item.DocumentsAttendusEtTeleverses.Add(MapDocument(document));
        }

        return item;
    }

    private ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel MapDocument(
        ShowClientDossierDocumentAttenduEtTeleverseDto document)
    {
        return new ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel
        {
            Id = document.Id,
            NomTypeDocumentAttendu = document.NomTypeDocumentAttendu,
            EstRequis = document.EstRequis,
            IsIncomplet = document.IsIncomplet,
            IsDocumentAttenduPresentDansDossierClient = document.IsDocumentAttenduPresentDansDossierClient,
            ExtensionTypeDocumentAttendu = document.ExtensionTypeDocumentAttendu,
            DocumentClientId = document.DocumentClientId,
            DocumentClientNomOriginal = document.DocumentClientNomOriginal,
            DocumentClientNomStandardise = document.DocumentClientNomStandardise,
            DocumentClientCheminPhysique = document.DocumentClientCheminPhysique,
            DocumentClientExtension = document.DocumentClientExtension,
            DocumentClientAjouteLe = document.DocumentClientAjouteLe,
            OuvrirDocumentCommand = new AsyncRelayCommand(() => OuvrirDocumentAsync(document.DocumentClientCheminPhysique))
        };
    }

    private async Task AjouterDocumentAuDossierAsync(string nomDossier)
    {
        var result = await _uploadClientDocumentDialogService.Show(new UploadClientDocumentDialogRequest(ClientId, nomDossier));
        if (result == true)
        {
            await ChargerDossiersAsync();
        }
    }

    private static Task OuvrirDocumentAsync(string cheminPhysique)
    {
        if (string.IsNullOrWhiteSpace(cheminPhysique))
        {
            return Task.CompletedTask;
        }

        try
        {
            Process.Start(new ProcessStartInfo
            {
                FileName = cheminPhysique,
                UseShellExecute = true
            });
        }
        catch
        {
        }

        return Task.CompletedTask;
    }
}
