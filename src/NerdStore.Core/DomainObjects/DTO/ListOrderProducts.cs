using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Core.DomainObjects.DTO
{
    public class ListOrderProducts
    {

        public Guid OrderId { get; set; }

        public ICollection<Item> items { get; set; }
    }
}
