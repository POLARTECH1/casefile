using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class TemplateDossierElementRepository : BaseRepo<TemplateDossierElement>, ITemplateDossierElementRepository
{
    public TemplateDossierElementRepository(CaseFileContext context) : base(context)
    {
    }
}
