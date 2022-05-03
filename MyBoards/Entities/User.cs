using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
        public class User
        {
            public Guid Id { get; set; }

            public string FullName { get; set; }
            public string Email { get; set; }

            public virtual Address Address { get; set; }
            public virtual List<WorkItem> WorkItems { get; set; } = new List<WorkItem>();
            public virtual List<Comment> Comments { get; set; } = new List<Comment>();
        }
}
