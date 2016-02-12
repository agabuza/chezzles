using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace chezzles.data.EF
{
    public interface IDbDataProvider : IUnitOfWork
    {
        DbSet<T> GetDbSet<T>() where T : class;
        DbEntityEntry<T> ContextEntry<T>(T entity) where T : class;
    }
}