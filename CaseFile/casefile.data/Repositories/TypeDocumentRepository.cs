using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class TypeDocumentRepository : BaseRepo<TypeDocument>, ITypeDocumentRepository
{
    public TypeDocumentRepository(CaseFileContext context) : base(context)
    {
    }
}
