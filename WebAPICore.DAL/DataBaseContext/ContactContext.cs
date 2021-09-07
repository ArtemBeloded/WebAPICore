using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using WebAPICore.DAL.Models;

namespace WebAPICore.DAL.DataBaseContext
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact { Id = 1, Name = "Artem", PhoneNumber = "123123", Marked = false },
                new Contact { Id = 2, Name = "Oleg", PhoneNumber = "321466", Marked = true }
                );
        }
    }
}
