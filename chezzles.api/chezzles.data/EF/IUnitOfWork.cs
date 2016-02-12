using System.Threading.Tasks;

namespace chezzles.data.EF
{
    public interface IUnitOfWork
    {
        void Save();
        Task SaveAsync();
    }
}