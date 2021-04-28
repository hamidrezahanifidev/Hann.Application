using System;
using System.Linq;
using Hahn.ApplicatonProcess.Application.Data.Context;
using Hahn.ApplicatonProcess.Application.Data.Repositories;
using Hahn.ApplicatonProcess.Application.Domain.Entities;

namespace Hahn.ApplicatonProcess.Application.Data.Repository
{
    public class AssetRepository : Repository<Asset>, IAssetRepository
    {

        public AssetRepository(ApplicationDbContext context) : base(context)
        { }

    }
}
