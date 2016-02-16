using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.data.Model
{
    [Table("Games")]
    public class GameDTO : IClientEntity
    {
        public int Id { get; set; }
        public string PgnString { get; set; }
    }
}
