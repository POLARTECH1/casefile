using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.Clients;

public sealed class ClientWithFiltersSpecification : Specification<Client>
{
    public ClientWithFiltersSpecification(
        string? nomPrenom = null,
        string? email = null,
        string? nomSchema = null,
        int? nombreDocuments = null)
    {
        Query.Include(c => c.SchemaClient)
            .Include(c => c.Dossiers)
            .ThenInclude(d => d.Documents);

        if (string.IsNullOrWhiteSpace(nomPrenom) == false)
        {
            var recherche = nomPrenom.Trim().ToLower();
            Query.Where(c =>
                (c.Nom + " " + c.Prenom).ToLower().Contains(recherche) ||
                (c.Prenom + " " + c.Nom).ToLower().Contains(recherche));
        }

        if (string.IsNullOrWhiteSpace(email) == false)
        {
            var emailRecherche = email.Trim().ToLower();
            Query.Where(c => c.Email.ToLower().Contains(emailRecherche));
        }

        if (string.IsNullOrWhiteSpace(nomSchema) == false)
        {
            var nomSchemaRecherche = nomSchema.Trim().ToLower();
            Query.Where(c => c.SchemaClient != null && c.SchemaClient.Nom.ToLower().Contains(nomSchemaRecherche));
        }

        if (nombreDocuments.HasValue)
        {
            Query.Where(c => c.Dossiers.SelectMany(d => d.Documents).Count() == nombreDocuments.Value);
        }
    }
}
