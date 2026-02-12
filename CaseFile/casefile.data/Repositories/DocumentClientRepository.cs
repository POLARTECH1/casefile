using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class DocumentClientRepository : BaseRepo<DocumentClient>, IDocumentClientRepository
{
    public DocumentClientRepository(CaseFileContext context) : base(context)
    {
    }
}
