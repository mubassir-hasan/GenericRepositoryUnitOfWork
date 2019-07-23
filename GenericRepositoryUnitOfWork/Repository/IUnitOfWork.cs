using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GenericRepositoryUnitOfWork.Repository
{
    public interface IUnitOfWork
    {
        IRepository<T> Repository<T>() where T : class;

        Task<int> CommitAsync();
        void Commit();

        void Rollback();
    }
}
