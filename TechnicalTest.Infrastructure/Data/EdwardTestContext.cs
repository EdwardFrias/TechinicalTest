using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using TechnicalTest.Core.Entities;

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
            modelBuilder.Entity<Person>(entity =>
            {
                entity.Property(e => e.DateOfBirth).HasColumnType("datetime");

                entity.Property(e => e.FullName)
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });

        }
    }
}
