using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.data.Model
{
    public class GameDTO : IClientEntity
    {
        public int Id { get; set; }
        public string PgnString { get; set; }
    }
}
