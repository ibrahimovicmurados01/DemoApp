using DemoApp.Entities.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasKey(x => x.Id);

            // Set Username property as unique
            builder.HasIndex(x => x.Username).IsUnique();

            // Set Email property as unique
            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.Username).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Email).IsRequired().HasMaxLength(250);
            builder.Property(x => x.Created).ValueGeneratedOnAdd();

            builder.HasMany(x => x.Contacts)       // One-to-many relationship
            .WithOne(x => x.User)           // Reference to the User entity in Contact
            .HasForeignKey(x => x.UserId)   // Foreign key in Contact
            .OnDelete(DeleteBehavior.Cascade);
        }



      
    }
}

