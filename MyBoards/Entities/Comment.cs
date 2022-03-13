using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
    public class Comment
    {
        public int Id { get; set; }

        public string Message { get; set; }
        public string Author { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

    }
}
