using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace chezzles.data
{
    public class Repository<T> : IRepository<T> where T : class, IClientEntity
    {
        private IDbConnection connection;

        public Repository(IDbConnection sqlConnection)
        {
            this.connection = sqlConnection;
        }

        public void Insert(T entity)
        {
            connection.Insert(entity);
        }

        public void Delete(T entity)
        {
            connection.Delete(entity);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
           return GetAll().Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return connection.GetAll<T>();
        }

        public T GetById(long id)
        {
            return connection.Get<T>(id);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return GetAll().FirstOrDefault(predicate);
        }

        public void Update(T entity)
        {
            connection.Update(entity);
        }

        public void Dispose()
        {
            this.connection?.Dispose();
        }
    }
}
