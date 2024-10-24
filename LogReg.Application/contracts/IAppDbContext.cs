namespace LogReg.Application.contracts;

public interface IAppDbContext
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
