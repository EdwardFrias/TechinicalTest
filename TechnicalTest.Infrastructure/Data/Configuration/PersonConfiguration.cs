using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechnicalTest.Core.Entities;

namespace TechnicalTest.Infrastructure.Data.Configuration
{
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("Person");

            builder.HasKey(x => x.Id);
            builder.Property(e => e.Id)
            .HasColumnName("Id");

            builder.Property(e => e.FullName)
                .IsRequired()
                .HasColumnName("FullName")
                .HasMaxLength(100)
                .IsUnicode(false);

            builder.Property(e => e.DateOfBirth)
                .HasColumnName("DateOfBirth")
                .HasColumnType("datetime");

        }
    }
}
