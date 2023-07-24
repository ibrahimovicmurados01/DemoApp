using DemoApp.Entities.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Models
{
    public class Contact :UniqueIdentifier, CreatedModifiedFeature, TombstoneFeature
    {
      
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        #region Dervived properties
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset Modified { get; set; }
        public bool Tombstoned { get; set; }
        #endregion

        // Foreign key to link the contact with the owning user
        public Guid UserId { get; set; }
        public User User { get; set; }



    }
}
