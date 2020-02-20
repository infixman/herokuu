using AspNetCoreDemoApp.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AspNetCoreDemoApp.Controllers
{
    [Route("api/[controller]")]
    public class RestaurantController : ControllerBase
    {

        [HttpGet]
        public IEnumerable<JapanRestaurant> Get(string week, string type, string star,
            bool parking, bool uber, bool deposit, string position, int page, int limit)
        {
            var result = new List<JapanRestaurant>();


            return result;
        }
    }
}
