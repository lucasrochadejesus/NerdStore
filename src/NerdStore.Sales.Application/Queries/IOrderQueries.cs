using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NerdStore.Sales.Application.Queries.ViewModels;

namespace NerdStore.Sales.Application.Queries
{
    public interface IOrderQueries
    {
        Task<ShoppingCartViewModel> GetShoppingCartByCustomerId(Guid customerId);

        Task<IEnumerable<OrderViewModel>> GetOrdersByCustomerId(Guid customerId);


    }
}
