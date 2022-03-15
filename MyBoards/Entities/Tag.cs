using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        public string Value { get; set; }
        public List<WorkItem> WorkItems { get; set; }
    }
}
