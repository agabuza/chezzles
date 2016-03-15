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
                new ChessSet { Name = "Classic",
                    FilePath = "hd/sprites.plist",
                    BoardPath = "tilemaps/board.tmx",
                    Description = "Classical chess board"
                },
                new ChessSet {
                    Name = "Contemp",
                    FilePath = "hd/sprites.plist",
                    BoardPath = "tilemaps/board_contemp.tmx",
                    Description = "Conteporary board"
                },
                new ChessSet {
                    Name = "Green",
                    FilePath = "hd/sprites.plist",
                    BoardPath = "tilemaps/board_green.tmx",
                    Description = "Classic green board"
                },
                new ChessSet {
                    Name = "Vintage",
                    FilePath = "hd/sprites.plist",
                    BoardPath = "tilemaps/board_old.tmx",
                    Description = "Vintage board"
                },
                new ChessSet {
                    Name = "Book",
                    FilePath = "hd/sprites.plist",
                    BoardPath = "tilemaps/board_book.tmx",
                    Description = "Puzzles from your bookshelf"
                }
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
