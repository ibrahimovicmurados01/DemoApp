using System;
using System.ComponentModel.DataAnnotations;

namespace DemoApp.Entities.Models.Contracts
{
    // The UniqueIdentifier class represents a simple entity with a unique identifier property.
    public class UniqueIdentifier
    {
        // The Key attribute marks the Id property as the primary key for the entity.
        // The Id property stores a unique identifier (GUID) for the entity, which is automatically generated.
        [Key]
        public Guid Id { get; set; }
    }
}
