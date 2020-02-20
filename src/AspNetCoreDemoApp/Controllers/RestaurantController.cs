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
        public IEnumerable<JapanRestaurant> Get([FromQuery]string week, [FromQuery]string type,
            [FromQuery]string star, [FromQuery]bool? parking, [FromQuery]bool? uber,
            [FromQuery]bool? deposit, [FromQuery]string position, [FromQuery]int? page,
            [FromQuery]int? limit)
        {
            return _logic.GetRestaurant(week, type, star, parking, uber, deposit, position, page, limit);
        }
    }
}
