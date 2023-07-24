using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DemoApp.Entities.Models.Contracts
{
    public interface TombstoneFeature
    {
        public bool Tombstoned { get; set; }
    }
}
