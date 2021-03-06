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
        public IEnumerable<JapanRestaurant> Get([FromQuery]string week, [FromQuery]string openTime, [FromQuery]string closeTime,
            [FromQuery]string type, [FromQuery]string star, [FromQuery]bool? parking, [FromQuery]bool? uber,
            [FromQuery]bool? deposit, [FromQuery]string position, [FromQuery]bool? orCondition,
            [FromQuery]int? page=1, [FromQuery]int? limit=10)
        {
            if (orCondition.HasValue
                && orCondition.Value == true)
            {
                return _logic.GetRestaurantOrCondition(week, openTime, closeTime, type, star, parking, uber, deposit, position, page.Value, limit.Value);
            }
            else
            {
                return _logic.GetRestaurant(week, openTime, closeTime, type, star, parking, uber, deposit, position, page.Value, limit.Value);
            }
        }
    }
}
