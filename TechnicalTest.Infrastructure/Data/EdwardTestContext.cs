using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TechnicalTest.Core.Entities;
using TechnicalTest.Infrastructure.Data.Configuration;

namespace TechnicalTest.Infrastructure.Data
{
    public partial class EdwardTestContext : DbContext
    {
        public EdwardTestContext()
        {
        }

        public EdwardTestContext(DbContextOptions<EdwardTestContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Person> People { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfiguration());
        }
    }
}
