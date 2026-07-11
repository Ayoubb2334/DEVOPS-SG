using Application.Interfaces;
using Domain.Interfaces;
using Persistance.Repositories;

namespace Persistance;

public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    private ISmartphoneRepository? _smartphones;

    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }

    public ISmartphoneRepository Smartphones => _smartphones ??= new SmartphoneRepository(_context);

    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => await _context.SaveChangesAsync(cancellationToken);
}
