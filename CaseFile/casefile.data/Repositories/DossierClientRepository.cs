using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class DossierClientRepository : BaseRepo<DossierClient>, IDossierClientRepository
{
    public DossierClientRepository(CaseFileContext context) : base(context)
    {
    }
}
