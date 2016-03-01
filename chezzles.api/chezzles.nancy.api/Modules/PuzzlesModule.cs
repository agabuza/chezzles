using chezzles.data;
using chezzles.data.Model;
using Nancy;
using System.Linq;

namespace chezzles.nancy.api.Modules
{
    public class PuzzlesModule : NancyModule
    {
        private IRepository<GameDTO> repo;

        public PuzzlesModule(IRepository<GameDTO> repo) 
            : base ("/api")
        {
            this.repo = repo;
            Get["/puzzles"] = _ =>
            {
                return this.repo.GetAll().ToList();
            };

            Get["/puzzles/{id:int}"] = _ =>
            {
                return this.repo.GetById(_.id);
            };
        }
    }
}