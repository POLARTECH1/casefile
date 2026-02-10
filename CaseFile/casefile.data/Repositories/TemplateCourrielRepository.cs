using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class TemplateCourrielRepository : BaseRepo<TemplateCourriel>, ITemplateCourrielRepository
{
    public TemplateCourrielRepository(CaseFileContext context) : base(context)
    {
    }
}
