using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Application.Queries.ViewModels
{
    public class ShoppingCartPaymentViewModel
    {
        public string CardName { get; set; }

        public string CardNumber { get; set; }

        public string ExpirationDate { get; set; }

        public string CvvCode { get; set; }

    }
}
