using Microsoft.EntityFrameworkCore;

namespace casefile.data.configuration;

public class CaseFileContext : DbContext
{
    public CaseFileContext() : base()
    {
    }

    public CaseFileContext(DbContextOptions<CaseFileContext> options) : base(options)
    {
    }

#if DEBUG
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (optionsBuilder.IsConfigured == false)
        {
            
        }
    }
#endif
}