using AspNetCoreDemoApp.Model;
using System.Collections.Generic;
using System.Linq;

namespace AspNetCoreDemoApp.Logic
{
    public class RestaurantLogic
    {
        private readonly AppDbContext _dbContext;
        public RestaurantLogic(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IEnumerable<JapanRestaurant> GetRestaurant(string week, string type, string star,
            bool parking, bool uber, bool deposit, string position, int page, int limit)
        {
            var result = _dbContext.JapanRestaurant.Where(x => x.Parking == true);
            return result;
        }
    }
}
