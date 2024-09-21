using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourcePlanner.Core.Repositories;

namespace ResourcePlanner.Core.UnitOfWork
{
    public interface IUnitOfWork : IDisposable
    {
        IResourceRepository Resources { get; }
        Task<int> CompleteAsync();  // Save changes to the database
    }
}
