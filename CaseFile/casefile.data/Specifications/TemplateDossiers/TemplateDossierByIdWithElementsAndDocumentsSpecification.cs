using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.TemplateDossiers;

public sealed class TemplateDossierByIdWithElementsAndDocumentsSpecification : Specification<TemplateDossier>
{
    public TemplateDossierByIdWithElementsAndDocumentsSpecification(Guid templateDossierId)
    {
        Query.Where(t => t.Id == templateDossierId)
            .Include(t => t.Elements)
            .ThenInclude(e => e.DocumentsAttendus);
    }
}
