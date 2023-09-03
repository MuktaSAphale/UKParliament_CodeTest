using System.Collections.Generic;
using System;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Emit;

namespace UKParliament.CodeTest.Data
{
    public class PersonManagerContext : DbContext
    {
        public PersonManagerContext(DbContextOptions<PersonManagerContext> options) : base(options)
        {
            People = Set<Person>();
            SeedInitialData();
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);            
        }

        private void SeedInitialData()
        {
            // Seed initial data
            People.AddRange(
                new Person { Id = 1, FirstName = "John", LastName = "Doe", Age = 30 },
                new Person { Id = 2, FirstName = "Jane", LastName = "Smith", Age = 25 },
                new Person { Id = 3, FirstName = "Foo", LastName = "Bar", Age = 40 }
            );
        }
    }
}