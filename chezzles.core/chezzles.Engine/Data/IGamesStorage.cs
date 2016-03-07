using chezzles.engine.Core.Game;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace chezzles.engine.Data
{
    public interface IGameStorage
    {
        Task<IEnumerable<Game>> GetAll();
        Task<Game> GetById(int Id);
    }
}