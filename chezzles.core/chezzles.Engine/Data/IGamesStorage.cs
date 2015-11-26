using chezzles.engine.Core.Game;
using System.Collections.Generic;

namespace chezzles.engine.Data
{
    public interface IGameStorage
    {
        IEnumerable<Game> All();
    }
}