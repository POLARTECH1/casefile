using System.Text;
using casefile.application.DTOs.DocumentClient;
using casefile.data.configuration;
using FluentResults;
using Microsoft.EntityFrameworkCore;

namespace casefile.application.Services;

public class ClientDocumentUploadService(IDbContextFactory<CaseFileContext> dbContextFactory) : IClientDocumentUploadService
{
    public async Task<Result<UploadClientDocumentResultDto>> ExecuteAsync(UploadClientDocumentRequestDto request)
    {
        if (request.ClientId == Guid.Empty)
        {
            return Result.Fail<UploadClientDocumentResultDto>("Le client est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(request.NomDossier))
        {
            return Result.Fail<UploadClientDocumentResultDto>("Le dossier cible est obligatoire.");
        }

        if (string.IsNullOrWhiteSpace(request.CheminFichierSource) || File.Exists(request.CheminFichierSource) == false)
        {
            return Result.Fail<UploadClientDocumentResultDto>("Le fichier selectionne est introuvable.");
        }

        if (request.TypeDocumentId is null || request.TypeDocumentId == Guid.Empty)
        {
            return Result.Fail<UploadClientDocumentResultDto>("Le type de document est obligatoire.");
        }

        try
        {
            await using var context = await dbContextFactory.CreateDbContextAsync();

            var clientExiste = await context.Clients.AnyAsync(c => c.Id == request.ClientId);
            if (clientExiste == false)
            {
                return Result.Fail<UploadClientDocumentResultDto>(
                    $"Client introuvable pour l'identifiant {request.ClientId}.");
            }

            var typeDocumentExiste = await context.TypesDocument.AnyAsync(t => t.Id == request.TypeDocumentId);
            if (typeDocumentExiste == false)
            {
                return Result.Fail<UploadClientDocumentResultDto>("Le type de document selectionne est introuvable.");
            }

            var nomDossier = request.NomDossier.Trim();
            var dossierExistant = await context.DossiersClients
                .Where(d =>
                    EF.Property<Guid>(d, "ClientId") == request.ClientId &&
                    d.Nom == nomDossier)
                .Select(d => new { d.Id, d.Nom })
                .FirstOrDefaultAsync();

            Guid dossierClientId;
            string dossierClientNom;
            if (dossierExistant is null)
            {
                dossierClientId = Guid.NewGuid();
                dossierClientNom = nomDossier;
                var cheminVirtuel = $"/clients/{request.ClientId:N}/{NormalizeForPath(nomDossier)}";

                await context.Database.ExecuteSqlInterpolatedAsync($@"
INSERT INTO dossiers_clients (id, nom, chemin_virtuel, client_id)
VALUES ({dossierClientId}, {dossierClientNom}, {cheminVirtuel}, {request.ClientId})");
            }
            else
            {
                dossierClientId = dossierExistant.Id;
                dossierClientNom = dossierExistant.Nom;
            }

            var nomOriginal = Path.GetFileName(request.CheminFichierSource);
            var extension = Path.GetExtension(nomOriginal).ToLowerInvariant();
            var nomBase = Path.GetFileNameWithoutExtension(nomOriginal);
            var nomStandardise = $"{NormalizeForPath(nomBase)}_{DateTime.UtcNow:yyyyMMddHHmmssfff}{extension}";

            var racineDocuments = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "CaseFile",
                "documents",
                request.ClientId.ToString("N"),
                NormalizeForPath(nomDossier));
            Directory.CreateDirectory(racineDocuments);

            var cheminPhysique = Path.Combine(racineDocuments, nomStandardise);
            File.Copy(request.CheminFichierSource, cheminPhysique, overwrite: false);

            var documentClientId = Guid.NewGuid();
            var ajouteLe = DateTime.UtcNow;

            await context.Database.ExecuteSqlInterpolatedAsync($@"
INSERT INTO documents_clients
    (id, nom_original, nom_standardise, chemin_physique, extension, ajoute_le, dossier_client_id, type_document_id)
VALUES
    ({documentClientId}, {nomOriginal}, {nomStandardise}, {cheminPhysique}, {extension}, {ajouteLe}, {dossierClientId}, {request.TypeDocumentId})");

            return Result.Ok(new UploadClientDocumentResultDto
            {
                DocumentClientId = documentClientId,
                CheminPhysique = cheminPhysique,
                NomOriginal = nomOriginal,
                NomDossier = dossierClientNom
            });
        }
        catch (DbUpdateConcurrencyException ex)
        {
            return Result.Fail<UploadClientDocumentResultDto>(
                $"Conflit de concurrence lors de l'enregistrement du document: {ex.Message}");
        }
        catch (Exception ex)
        {
            return Result.Fail<UploadClientDocumentResultDto>(
                $"Erreur lors du televersement du document: {ex.Message}");
        }
    }

    private static string NormalizeForPath(string value)
    {
        var trimmed = value.Trim();
        if (trimmed.Length == 0)
        {
            return "document";
        }

        var builder = new StringBuilder(trimmed.Length);
        foreach (var ch in trimmed)
        {
            if (char.IsLetterOrDigit(ch))
            {
                builder.Append(char.ToLowerInvariant(ch));
            }
            else if (ch is ' ' or '-' or '_' or '.')
            {
                builder.Append('_');
            }
        }

        var normalized = builder.ToString().Trim('_');
        return string.IsNullOrWhiteSpace(normalized) ? "document" : normalized;
    }
}
