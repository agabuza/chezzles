using chezzles.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;
using System.ComponentModel.DataAnnotations.Schema;

namespace chezzles.data.EF
{
    public class GamesStoreContext : DbContext, IDbDataProvider
    {
        public GamesStoreContext()
            : base("GamesStoreContext")
        {
        }

        internal DbSet<Group> Groups { get; set; }
        internal DbSet<GameDTO> Games { get; set; }

        public DbEntityEntry<T> ContextEntry<T>(T entity) where T : class
        {
            return this.Entry<T>(entity);
        }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save()
        {
            SaveChanges();
        }

        public Task SaveAsync()
        {
            return SaveChangesAsync();
        }
    }
}
