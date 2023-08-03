using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Models.Contracts
{
 
    // The TombstoneFeature interface defines a property to track the tombstone status of an entity.
    // In the context of a delete operation, when the Tombstoned property is changed to true,
    // it indicates that the entity should be considered as tombstoned (soft-deleted).
    public interface TombstoneFeature
    {
        // Gets or sets a boolean value indicating whether the entity is tombstoned (soft-deleted).
        bool Tombstoned { get; set; }
    }
}
