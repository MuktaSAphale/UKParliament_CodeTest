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
        }

        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}