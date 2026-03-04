using casefile.application.DTOs.Client;
using casefile.application.UseCases.Interfaces;
using casefile.data.Repositories.Interface;
using casefile.data.Specifications.Clients;
using casefile.domain.model;
using FluentResults;

namespace casefile.application.UseCases.ClientUseCases;

public class GetClientDossiers(IClientRepository clientRepository) : IGetClientDossiers
{
    public async Task<Result<IEnumerable<ShowClientDossierDto>>> ExecuteAsync(Guid clientId)
    {
        var clientsResult = await clientRepository.GetAllAsync(new ClientByIdWithDetailsSpecification(clientId));
        if (clientsResult.IsFailed)
        {
            return Result.Fail<IEnumerable<ShowClientDossierDto>>(clientsResult.Errors);
        }

        var client = clientsResult.Value.FirstOrDefault();
        if (client is null)
        {
            return Result.Fail<IEnumerable<ShowClientDossierDto>>(
                $"Client introuvable pour l'identifiant {clientId}.");
        }

        var templateElementsByName = client.TemplateDossier?.Elements
            .GroupBy(e => e.Nom, StringComparer.OrdinalIgnoreCase)
            .ToDictionary(g => g.Key, g => g.SelectMany(e => e.DocumentsAttendus).ToList(), StringComparer.OrdinalIgnoreCase)
            ?? new Dictionary<string, List<DocumentAttendu>>(StringComparer.OrdinalIgnoreCase);

        var dossiers = client.Dossiers
            .OrderBy(d => d.Nom)
            .Select(dossier =>
            {
                var attendus = templateElementsByName.TryGetValue(dossier.Nom, out var value)
                    ? value
                    : new List<DocumentAttendu>();

                var documents = dossier.Documents.ToList();
                var attendusMappees = new List<ShowClientDossierDocumentAttenduEtTeleverseDto>();
                var documentsUtilises = new HashSet<Guid>();

                foreach (var attendu in attendus)
                {
                    var documentAssocie = documents.FirstOrDefault(d =>
                        attendu.IdTypeDocument is not null &&
                        d.TypeDocument?.Id == attendu.IdTypeDocument &&
                        documentsUtilises.Contains(d.Id) == false);

                    if (documentAssocie is not null)
                    {
                        documentsUtilises.Add(documentAssocie.Id);
                    }

                    attendusMappees.Add(new ShowClientDossierDocumentAttenduEtTeleverseDto
                    {
                        Id = attendu.Id,
                        NomTypeDocumentAttendu = attendu.TypeDocument?.Nom ?? "Type de document inconnu",
                        EstRequis = attendu.EstRequis,
                        IsDocumentAttenduPresentDansDossierClient = documentAssocie is not null,
                        IsIncomplet = attendu.EstRequis && documentAssocie is null,
                        ExtensionTypeDocumentAttendu = attendu.TypeDocument?.ExtensionsPermises ?? string.Empty,
                        DocumentClientId = documentAssocie?.Id,
                        DocumentClientNomOriginal = documentAssocie?.NomOriginal ?? string.Empty,
                        DocumentClientNomStandardise = documentAssocie?.NomStandardise ?? string.Empty,
                        DocumentClientCheminPhysique = documentAssocie?.CheminPhysique ?? string.Empty,
                        DocumentClientExtension = documentAssocie?.Extension ?? string.Empty,
                        DocumentClientAjouteLe = documentAssocie?.AjouteLe
                    });
                }

                foreach (var documentSansAttendu in documents.Where(d => documentsUtilises.Contains(d.Id) == false))
                {
                    attendusMappees.Add(new ShowClientDossierDocumentAttenduEtTeleverseDto
                    {
                        Id = documentSansAttendu.Id,
                        NomTypeDocumentAttendu = documentSansAttendu.TypeDocument?.Nom ?? documentSansAttendu.NomOriginal,
                        EstRequis = false,
                        IsDocumentAttenduPresentDansDossierClient = true,
                        IsIncomplet = false,
                        ExtensionTypeDocumentAttendu = documentSansAttendu.TypeDocument?.ExtensionsPermises ??
                                                       documentSansAttendu.Extension,
                        DocumentClientId = documentSansAttendu.Id,
                        DocumentClientNomOriginal = documentSansAttendu.NomOriginal,
                        DocumentClientNomStandardise = documentSansAttendu.NomStandardise,
                        DocumentClientCheminPhysique = documentSansAttendu.CheminPhysique,
                        DocumentClientExtension = documentSansAttendu.Extension,
                        DocumentClientAjouteLe = documentSansAttendu.AjouteLe
                    });
                }

                var nombreRequis = attendus.Count(a => a.EstRequis);
                var requisManquants = attendusMappees.Count(a => a.EstRequis && a.IsDocumentAttenduPresentDansDossierClient == false);

                return new ShowClientDossierDto
                {
                    Nom = dossier.Nom,
                    NombreDocuments = dossier.Documents.Count,
                    NombreDocumentRequis = nombreRequis,
                    IsDossierComplet = requisManquants == 0,
                    DocumentsAttendusEtTeleverses = attendusMappees
                };
            });

        return Result.Ok<IEnumerable<ShowClientDossierDto>>(dossiers);
    }
}
