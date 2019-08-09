using System;
namespace SC.Repository
{
    public interface IUnitOfWork
    {
        void BeginTransaction();
        void Commit();
        void Dispose();
        GenericRepository<T> GetRepository<T>() where T : class;
        void Save();
    }
}
