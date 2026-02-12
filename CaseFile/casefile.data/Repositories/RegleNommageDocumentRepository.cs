using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class RegleNommageDocumentRepository : BaseRepo<RegleNommageDocument>, IRegleNommageDocumentRepository
{
    public RegleNommageDocumentRepository(CaseFileContext context) : base(context)
    {
    }
}
