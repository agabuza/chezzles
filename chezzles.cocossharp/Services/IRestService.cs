using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.cocossharp.Services
{
    public interface IRestService<T> : IDisposable
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int Id);
        Task<T> Next();
    }
}
