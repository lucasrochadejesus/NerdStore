using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class OrderViewModel
    {
        public int OrderId { get; set; }

        public decimal Total { get; set; }

        public DateTime DateCreate { get;set; }

        public int OrderStatus { get; set; }
    }
}
