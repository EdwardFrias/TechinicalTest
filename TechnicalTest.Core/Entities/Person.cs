using System;
using System.Collections.Generic;

namespace TechnicalTest.Core.Entities
{
    public partial class Person
    {
        public int Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }
    }
}
