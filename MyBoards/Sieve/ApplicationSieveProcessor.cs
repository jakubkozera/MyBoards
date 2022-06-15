using Microsoft.Extensions.Options;
using MyBoards.Entities;
using Sieve.Models;
using Sieve.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards.Sieve
{
    public class ApplicationSieveProcessor : SieveProcessor
    {
        public ApplicationSieveProcessor(IOptions<SieveOptions> options) : base(options)
        {
        }

        protected override SievePropertyMapper MapProperties(SievePropertyMapper mapper)
        {
            mapper.Property<Epic>(e => e.Priority)
                .CanSort()
                .CanFilter();

            mapper.Property<Epic>(e => e.Area)
                .CanSort()
                .CanFilter();

            mapper.Property<Epic>(e => e.StartDate)
               .CanSort()
               .CanFilter();

            mapper.Property<Epic>(e => e.Author.FullName)
               .CanSort()
               .CanFilter()
               .HasName("authorFullName"); // "Author.FullName" -> "authorFullName"

            return mapper;
        }
    }
}