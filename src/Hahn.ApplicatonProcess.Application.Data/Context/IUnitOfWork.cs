using System;
using Hahn.ApplicatonProcess.Application.Data.Repository;

namespace Hahn.ApplicatonProcess.Application.Data.Context
{
    public interface IUnitOfWork : IDisposable
    {
        IAssetRepository Assets { get; }
        int SaveChanges();
    }
}
