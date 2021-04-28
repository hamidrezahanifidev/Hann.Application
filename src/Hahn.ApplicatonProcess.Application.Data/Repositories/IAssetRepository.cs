using System;
using Hahn.ApplicatonProcess.Application.Data.Repositories;
using Hahn.ApplicatonProcess.Application.Domain.Entities;

namespace Hahn.ApplicatonProcess.Application.Data.Repository
{
    public interface IAssetRepository : IRepository<Asset>
    {
        // We can add any entity related operations like get most used assets or ... here
    }
}
