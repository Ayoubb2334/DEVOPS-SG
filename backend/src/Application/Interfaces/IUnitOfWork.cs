using Domain.Interfaces;

namespace Application.Interfaces;

public interface IUnitOfWork
{
    ISmartphoneRepository Smartphones { get; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}