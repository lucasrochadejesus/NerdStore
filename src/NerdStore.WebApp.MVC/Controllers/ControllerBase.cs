using Microsoft.AspNetCore.Mvc;

namespace NerdStore.WebApp.MVC.Controllers
{
    public abstract class ControllerBase: Controller
    {
        // Customer ID (simulate customer ID)
        protected Guid CustomerId = Guid.Parse("BBD6AE49-712B-4F6A-BB55-0414089D7DFD");
    }
}
