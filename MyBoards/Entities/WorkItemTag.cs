using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
    public class WorkItemTag
    {
        public virtual WorkItem WorkItem { get; set; }
        public int WorkItemId { get; set; }

        public virtual Tag Tag { get; set; }
        public int TagId { get; set; }
        public DateTime PublicationDate { get; set; }
    }
}
