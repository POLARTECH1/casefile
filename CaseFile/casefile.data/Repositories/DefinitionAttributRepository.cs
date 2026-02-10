using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class DefinitionAttributRepository : BaseRepo<DefinitionAttribut>, IDefinitionAttributRepository
{
    public DefinitionAttributRepository(CaseFileContext context) : base(context)
    {
    }
}
