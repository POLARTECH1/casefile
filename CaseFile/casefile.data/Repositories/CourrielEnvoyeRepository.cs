using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class CourrielEnvoyeRepository : BaseRepo<CourrielEnvoye>, ICourrielEnvoyeRepository
{
    public CourrielEnvoyeRepository(CaseFileContext context) : base(context)
    {
    }
}
