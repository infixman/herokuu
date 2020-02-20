using AspNetCoreDemoApp.Logic;
using AspNetCoreDemoApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {
        private RestaurantLogic _logic;

        public RestaurantController(AppDbContext dbContext)
        {
            _logic = new RestaurantLogic(dbContext);
        }

        [HttpGet]
        public IEnumerable<JapanRestaurant> Get(string week, string type, string star,
            bool parking, bool uber, bool deposit, string position, int page, int limit)
        {
            return _logic.GetRestaurant(week, type, star, parking, uber, deposit, position, page, limit);
        }
    }
}
