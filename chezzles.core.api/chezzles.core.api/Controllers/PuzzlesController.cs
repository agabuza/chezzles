using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace chezzles.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuzzlesController : ControllerBase
    {
        private IRepository<GameDTO> repo;

        public PuzzlesController(IRepository<GameDTO> repo)
        {
            this.repo = repo;
        }

        [HttpGet]
        public IList<GameDTO> GetAll()
        {
            return this.repo.GetAll().ToList();
        }

        [HttpGet]
        public GameDTO GetNextPuzzle(int id)
        {
            return this.repo.GetById(_.id);
        }
    }
}