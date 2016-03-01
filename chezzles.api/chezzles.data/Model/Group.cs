using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chezzles.data.Model
{
    public class Group : IClientEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
