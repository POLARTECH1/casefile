using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class DocumentAttenduRepository : BaseRepo<DocumentAttendu>, IDocumentAttenduRepository
{
    public DocumentAttenduRepository(CaseFileContext context) : base(context)
    {
    }
}
