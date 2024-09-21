using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourcePlanner.Core.Repositories;

namespace ResourcePlanner.Core.UnitOfWork
{
    public sealed class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public IResourceRepository Resources { get; }

        public UnitOfWork(ApplicationDbContext context, IResourceRepository resourceRepository)
        {
            _context = context;
            Resources = resourceRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
