using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.AppExceptions;
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
            var persons = await _context.People.ToListAsync();
            return persons;
        }
        public async Task<Person> GetPersonById(int personId)
        {
            
            var person = await _context.People.FirstOrDefaultAsync(x => x.Id == personId);
            return person;
        }

        public async Task<Person> DeletePerson(int personId)
        {
            var person = await GetPersonById(personId);
            _context.People.Remove(person);

            await _context.SaveChangesAsync();
            return person;
        }

        public async Task InsertPerson(Person person)
        {
            _context.People.Add(person);
            await _context.SaveChangesAsync();
        }

        public async Task<Person> UpdatePerson(Person person)
        {
            var currentPerson = await GetPersonById(person.Id);
            currentPerson.FullName = person.FullName;
            currentPerson.DateOfBirth = person.DateOfBirth;

            await _context.SaveChangesAsync(); 
            return currentPerson;
        }
    }
}
