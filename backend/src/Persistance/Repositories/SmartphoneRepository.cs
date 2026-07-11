using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Repositories;

public class SmartphoneRepository : Repository<Smartphone>, ISmartphoneRepository
{
    public SmartphoneRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IReadOnlyList<Smartphone>> GetByMarqueAsync(string marque, CancellationToken cancellationToken = default)
        => await _context.Smartphones
            .AsNoTracking()
            .Where(s => s.Marque.ToLower() == marque.ToLower())
            .ToListAsync(cancellationToken);
}