using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Models.Contracts
{
    
    // The CreatedModifiedFeature interface defines properties to track the creation and modification timestamps of an entity.
    public interface CreatedModifiedFeature
    {
        // Gets or sets the DateTimeOffset indicating when the entity was created.
        DateTimeOffset Created { get; set; }

        // Gets or sets the DateTimeOffset indicating when the entity was last modified.
        DateTimeOffset Modified { get; set; }
    }

}
