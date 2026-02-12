using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class ValeurAttributClientRepository : BaseRepo<ValeurAttributClient>, IValeurAttributClientRepository
{
    public ValeurAttributClientRepository(CaseFileContext context) : base(context)
    {
    }
}
