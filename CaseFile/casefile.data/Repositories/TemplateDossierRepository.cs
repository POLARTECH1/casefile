using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class TemplateDossierRepository : BaseRepo<TemplateDossier>, ITemplateDossierRepository
{
    public TemplateDossierRepository(CaseFileContext context) : base(context)
    {
    }
}
