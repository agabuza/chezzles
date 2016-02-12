using chezzles.data.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.data.EF
{
    public class ModelContext : DbContext, IDbDataProvider
    {
        internal DbSet<GameDTO> Games { get; set; }

        public DbSet<T> GetDbSet<T>() where T : class
        {
            return this.Set<T>();
        }

        public void Save()
        {
            this.SaveChanges();
        }

        public void SaveAsync()
        {
            this.SaveChangesAsync();
        }
    }
}
