using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.Entities;
using TechnicalTest.Core.Interfaces;
using TechnicalTest.Infrastructure.Data;

namespace TechnicalTest.Infrastructure.Repositories
{
    public class PersonServices : IPersonServices
    {
        private readonly EdwardTestContext _context;
        public PersonServices(EdwardTestContext context)
        { 
            _context = context;
        }
        public async Task<IEnumerable<Person>> GetAllPerson()
        {
            var persons = await  _context.People.ToListAsync();
            return persons;
        }
    }
}
