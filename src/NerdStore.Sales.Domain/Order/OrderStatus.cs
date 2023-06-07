using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.Sales.Domain.Order
{
    public enum OrderStatus
    {
        Quote = 0,
        Started = 1,
        Paid = 2,
        Processing = 3,
        Shipped = 4,
        Cancelled = 5
    }
}
