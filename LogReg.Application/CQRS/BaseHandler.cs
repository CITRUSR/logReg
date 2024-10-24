using LogReg.Application.contracts;

namespace LogReg.Application.CQRS;

public class BaseHandler(IAppDbContext dbContext)
{
    protected readonly IAppDbContext DbContext = dbContext;
}
