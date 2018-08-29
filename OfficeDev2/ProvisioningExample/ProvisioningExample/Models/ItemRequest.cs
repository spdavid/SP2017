using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProvisioningExample.Models
{
    public class ItemRequest
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string OwnerEmail { get; set; }

    }
}
