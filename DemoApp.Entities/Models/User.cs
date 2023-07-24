using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DemoApp.Entities.Models.Contracts;

namespace DemoApp.Entities.Models
{
  
    public class User : UniqueIdentifier, CreatedModifiedFeature, TombstoneFeature
    {
        [Required] public string Username { get; set; }

        // TODO: Make it unique later
        [EmailAddress][Required] public string Email { get; set; }
        [Required] public string HashedPassword { get; set; }


        public DateTimeOffset? LastSigninDate { get; set; }

        #region  Dervived properties
        public DateTimeOffset Created { get; set; }= DateTimeOffset.Now;
        public DateTimeOffset Modified { get; set; }
        public bool Tombstoned { get; set; }
        #endregion

        public ICollection<Contact> Contacts { get; set; }
    }
}
