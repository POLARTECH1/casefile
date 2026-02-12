using casefile.data.configuration;
using casefile.data.Repositories.Interface;
using casefile.domain.model;

namespace casefile.data.Repositories;

public class ProfilEntrepriseRepository : BaseRepo<ProfilEntreprise>, IProfilEntrepriseRepository
{
    public ProfilEntrepriseRepository(CaseFileContext context) : base(context)
    {
    }
}
