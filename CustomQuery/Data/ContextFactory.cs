using Microsoft.EntityFrameworkCore;

namespace CustomQuery.Data;

public class ContextFactory
{
    private readonly DbContextOptions<Context> _options;

    public ContextFactory()
    {
        _options = new DbContextOptionsBuilder<Context>().UseInMemoryDatabase("test").Options;
    }
 
    public Context GetNewDbContext()
    {
        return new Context(_options);
    }
}