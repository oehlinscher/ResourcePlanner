using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ResourcePlanner.Core.Entities;

namespace ResourcePlanner.Core.Repositories
{
    public interface IResourceRepository : IGenericRepository<Resource>
    {
        // Add any custom methods specific to Resource
    }
}
