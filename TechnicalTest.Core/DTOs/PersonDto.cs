using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechnicalTest.Core.DTOs
{
    public class PersonDto
    {
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
