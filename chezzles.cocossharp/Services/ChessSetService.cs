using chezzles.cocossharp.Pieces.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(chezzles.cocossharp.Services.ChessSetService))]
namespace chezzles.cocossharp.Services
{
    public class ChessSetService : IRestService<ChessSet>
    {
        public Task<List<ChessSet>> GetAll()
        {
            var list = new List<ChessSet>()
            {
                new ChessSet { Name = "Classic", FilePath = "sprites.png", Description = "Classical chess set" },
                new ChessSet { Name = "Book", FilePath = "book_sprites.png", Description = "Puzzles from your bookshelf" }
            };

            return Task.FromResult(list);
        }

        public Task<ChessSet> GetById(int Id)
        {
            throw new NotImplementedException();
        }

        public Task<ChessSet> Next()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
