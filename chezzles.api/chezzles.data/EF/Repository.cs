using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.data.EF
{
    public class Repository<T> : IRepository<T> where T : class, IClientEntity
    {
        private DbSet<T> context;
        private IUnitOfWork unitOfWork;
        private IDbDataProvider dataProvider;

        public Repository(IDbDataProvider dataProvider)
        {
            this.context = dataProvider.GetDbSet<T>();
            this.dataProvider = dataProvider;
            unitOfWork = dataProvider as IUnitOfWork;
        }

        public void Insert(T entity)
        {
            context.Add(entity);
        }

        public void Delete(T entity)
        {
            context.Remove(entity);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return context.Where(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return context;
        }

        public T GetById(long id)
        {
            return context.FirstOrDefault(x => x.Id == id);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return context.FirstOrDefault(predicate);
        }

        public void Attach(T entity)
        {
            var entry = this.dataProvider.ContextEntry(entity);

            if (entry.State == EntityState.Detached)
            {
                var currentEntry = this.context.Find(entity.Id);
                if (currentEntry != null)
                {
                    var attachedEntry = this.dataProvider.ContextEntry(currentEntry);
                    attachedEntry.CurrentValues.SetValues(entity);
                }
                else
                {
                    this.context.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
        }

        private object GetPrimaryKey(DbEntityEntry<T> entry)
        {
            throw new NotImplementedException();
        }

        #region IUnitOfWork Members

        public void Save()
        {
            if (unitOfWork != null)
                unitOfWork.Save();
        }

        public Task SaveAsync()
        {
            if (unitOfWork != null)
                 return unitOfWork.SaveAsync();

            return Task.Delay(0);
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
