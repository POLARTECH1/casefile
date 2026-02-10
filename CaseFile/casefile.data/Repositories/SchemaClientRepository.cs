using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class SchemaClientRepository : BaseRepo<SchemaClient>, ISchemaClientRepository
{
    public SchemaClientRepository(CaseFileContext context) : base(context)
    {
    }
}
