using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.core.data.Model
{
    public class GameDTO
    {
        public int Id { get; set; }
        public string PgnString { get; set; }
    }
}
