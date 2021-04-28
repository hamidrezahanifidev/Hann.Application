using System;
using Hahn.ApplicatonProcess.Application.Data.Repository;

namespace Hahn.ApplicatonProcess.Application.Data.Context
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Assets = new AssetRepository(_context);
        }

        public IAssetRepository Assets { get; private set; }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
