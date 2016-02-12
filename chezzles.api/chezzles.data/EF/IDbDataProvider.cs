using System.Data.Entity;

namespace chezzles.data.EF
{
    public interface IDbDataProvider : IUnitOfWork
    {
        DbSet<T> GetDbSet<T>() where T : class;
    }
}