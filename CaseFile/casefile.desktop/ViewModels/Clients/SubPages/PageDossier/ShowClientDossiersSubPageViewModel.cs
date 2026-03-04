using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;

namespace casefile.desktop.ViewModels.Clients.SubPages.PageDossier;

public sealed class ShowClientDossiersSubPageViewModel : ViewModelBase
{
    private readonly IGetClientDossiers _getClientDossiers;

    public ShowClientDossiersSubPageViewModel(Guid clientId, IGetClientDossiers getClientDossiers)
    {
        ClientId = clientId;
        _getClientDossiers = getClientDossiers;
        _ = ChargerDossiersAsync();
    }

    public Guid ClientId { get; }

    /// <summary>
    /// L'ensemble des dossiers virtuels associés au client.
    /// </summary>
    public ObservableCollection<ShowClientDossierSubPageItemViewModel> Dossiers { get; } =
        new ObservableCollection<ShowClientDossierSubPageItemViewModel>();

    public async Task ChargerDossiersAsync()
    {
        var result = await _getClientDossiers.ExecuteAsync(ClientId);
        if (result.IsFailed)
        {
            return;
        }

        Dossiers.Clear();
        foreach (var dossier in result.Value)
        {
            Dossiers.Add(Map(dossier));
        }
    }

    private static ShowClientDossierSubPageItemViewModel Map(ShowClientDossierDto dossier)
    {
        var item = new ShowClientDossierSubPageItemViewModel
        {
            Nom = dossier.Nom,
            NombreDocuments = dossier.NombreDocuments + " document(s)",
            NombreDocumentRequis = dossier.NombreDocumentRequis + " requis",
            IsDossierComplet = dossier.IsDossierComplet
        };

        foreach (var document in dossier.DocumentsAttendusEtTeleverses)
        {
            item.DocumentsAttendusEtTeleverses.Add(Map(document));
        }

        return item;
    }

    private static ShowClientDossierSubPageItemDocumentAttenduEtDocumentsTeleverseViewModel Map(
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
            DocumentClientAjouteLe = document.DocumentClientAjouteLe
        };
    }
}