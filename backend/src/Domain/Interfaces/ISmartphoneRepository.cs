using Domain.Entities;

namespace Domain.Interfaces;

public interface ISmartphoneRepository : IRepository<Smartphone>
{
    Task<IReadOnlyList<Smartphone>> GetByMarqueAsync(string marque, CancellationToken cancellationToken = default);
}