using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models
{
    public class Ratings
    {
        public int Id { get; set; }
        public int RestaurantId { get; set; }
        public double FoodScore { get; set; }
        public double CleanlinessScore { get; set; }
        public double AtmosphereScore { get; set; }
        public virtual Restaurants Restaurant { get; set; }
    }
}