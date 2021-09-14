using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using chezzles.core.api.Dto;
using System;

namespace chezzles.core.api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PuzzlesController : ControllerBase
    {

        [HttpGet]
        public IList<Game> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        public Game GetNextPuzzle(int id)
        {
            throw new NotImplementedException();
        }
    }
}