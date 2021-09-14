using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.core.api.Services
{
    public interface IPuzzleService<T> : IDisposable
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int Id);
        Task<T> Next();
    }
}
