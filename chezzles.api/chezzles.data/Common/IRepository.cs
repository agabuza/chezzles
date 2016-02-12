using System;
using System.Collections.Generic;
using Dapper.Contrib.Extensions;

namespace chezzles.data
{
    public interface IRepository<T> : IDisposable
    {
        void Insert(T entity);
        void Delete(T entity);
        void Update(T entity);
        IEnumerable<T> Find(Func<T, bool> predicate);
        IEnumerable<T> GetAll();
        T GetById(long id);
        T FirstOrDefault(Func<T, bool> predicate);
    }

    public interface IClientEntity
    {
        [Key]
        int Id { get; set; }
    }
}
