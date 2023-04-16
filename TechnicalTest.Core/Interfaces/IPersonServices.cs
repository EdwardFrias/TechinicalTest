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
    }
}
