using Ardalis.Specification;
using casefile.domain.model;

namespace casefile.data.Specifications.TemplateDossiers;

public sealed class TemplateDossierWithElementsAndDocumentsSpecification : Specification<TemplateDossier>
{
    public TemplateDossierWithElementsAndDocumentsSpecification()
    {
        Query.Include(t => t.Elements)
            .ThenInclude(e => e.DocumentsAttendus);
    }
}
