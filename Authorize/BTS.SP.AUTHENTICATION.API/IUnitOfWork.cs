﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BTS.SP.AUTHENTICATION.API;

namespace  BTS.SP.AUTHENTICATION.API
{
    public interface IUnitOfWork : IUnitOfWorkForService
    {
        void Save();
        Task<int> SaveAsync();
        Task<int> SaveAsync(CancellationToken cancellationToken);
        void Dispose(bool disposing);
    }

    // To be used in services e.g. ICustomerService, does not expose Save()
    // or the ability to Commit unit of work
    public interface IUnitOfWorkForService : IDisposable
    {
        IRepository<TEntity> Repository<TEntity>() where TEntity : EntityBase;
    }
}
