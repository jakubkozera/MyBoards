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
        public User Author { get; set; }
        public Guid AuthorId { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

    }
}
