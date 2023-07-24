using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Models.Contracts
{
    public class UniqueIdentifier
    {
        [Key] public Guid Id { get; set; }
    }
}
