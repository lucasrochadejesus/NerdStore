using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class ShoppingCartViewModel
    {

        public Guid OrderId { get; set; }

        public Guid CustomerId { get; set; }

        public decimal SubTotal { get; set; }

        public decimal Total { get; set; }

        public decimal Discount { get; set; }
        
        public string Coupon { get; set; }

        public List<ShoppingCartItemViewModel> Items { get; set; } = new List<ShoppingCartItemViewModel>();

        public ShoppingCartPaymentViewModel Payment { get; set; }
    }
}
