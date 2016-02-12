using chezzles.data;
using chezzles.data.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace chezzles.api.Controllers
{
    public class PuzzlesController : ApiController
    {
        private IRepository<GameDTO> repository;

        public PuzzlesController(IRepository<GameDTO> repo)
        {
            this.repository = repo;
        }

        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return repository.GetAll().Select(x => x.PgnString);
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return repository.GetById(id)?.PgnString;
        }
    }
}