using Bogus;
using MyBoards.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBoards
{
    public class DataGenerator
    {
        public static void Seed(MyBoardsContext context)
        {
            var locale = "pl";

            Randomizer.Seed = new Random(911);

            var addressGenerator = new Faker<Address>(locale)
                //.StrictMode(true)
                .RuleFor(a => a.City, f => f.Address.City())
                .RuleFor(a => a.Country, f => f.Address.Country())
                .RuleFor(a => a.PostalCode, f => f.Address.ZipCode())
                .RuleFor(a => a.Street, f => f.Address.StreetName());

            var userGenerator = new Faker<User>(locale)
                .RuleFor(u => u.Email, f => f.Person.Email)
                .RuleFor(u => u.FullName, f => f.Person.FullName)
                .RuleFor(u => u.Address, f => addressGenerator.Generate());

            var users = userGenerator.Generate(100);

            context.AddRange(users);
            context.SaveChanges();
        }
    }
}