using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string PostalCode { get; set; }
        public virtual User User { get; set; }
        public Guid UserId { get; set; }
        public Coordinate Coordinate { get; set; }
    }

    public class Coordinate 
    {
        public decimal? Longitude { get; set; }
        public decimal? Latitude { get; set; }
    }

}
