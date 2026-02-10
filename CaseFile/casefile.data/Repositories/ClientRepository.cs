using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class ClientRepository : BaseRepo<Client>, IClientRepository
{
    public ClientRepository(CaseFileContext context) : base(context)
    {
    }
}
