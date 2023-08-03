using DemoApp.Entities.Configurations;
using DemoApp.Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
            GenerateFirstUserData(modelBuilder);

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        private void GenerateFirstUserData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = Guid.Parse("B7BB5FE4-11D7-4E48-B9D5-1E4CF76FD751"),
                    Username = "admin",
                    Email = "administrator@pa.local",
                    // 123456
                    HashedPassword = "$2a$11$nIC0rs6cIFQVKOzEiQpweexL9GZZpm1E1mpHMIrMZVnodYtBYD5.i",
                    Created = DateTimeOffset.UtcNow,
                    Modified = DateTimeOffset.UtcNow,
                    Tombstoned = false
                });

            modelBuilder.Entity<Contact>().HasData(
                GenerateContact("535c3d4e-d84e-11ec-9d64-0242ac120002", "Azer","Halovic","test@gmail.com","559001122"),
                GenerateContact("535c3d4e-d84e-11ec-9d64-0242ac120003", "Veli", "Halovic", "test3@gmail.com", "559001122")
            );
        }

        private static Contact GenerateContact(string id, string firstName,string lastName,string email,string pNumber)
        {
            return new Contact
            {
                Id = Guid.Parse(id),
                UserId = Guid.Parse("B7BB5FE4-11D7-4E48-B9D5-1E4CF76FD751"),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                PhoneNumber = pNumber,
                Created = DateTimeOffset.UtcNow,
                Modified = DateTimeOffset.UtcNow,
                Tombstoned = false
            };
        }

    }
}
