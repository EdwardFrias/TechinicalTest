using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.Entities;

namespace TechnicalTest.Core.Interfaces
{
    public interface IPersonServices
    {
        Task<IEnumerable<Person>> GetAllPerson();
        Task<Person> GetPersonById(int personId);
        Task InsertPerson(Person person);
        Task<Person> UpdatePerson(Person person);
        Task<Person> DeletePerson(int personId);
    }
}
