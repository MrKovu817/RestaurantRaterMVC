using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RestaurantRaterMVC.Models
{
    public class Restaurants
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [Required]
        [MaxLength(100)]
        public string Location { get; set; }
        public virtual List<Ratings> Ratings { get; set; }
        public double AverageFoodScore=> Ratings.Average(r => r.FoodScore);
        public double AverageCleanlinessScore
        {
            get
            {
                return (Ratings.Count > 0) ?Ratings.Select(r => r.CleanlinessScore).Sum() / Ratings.Count:0;
            }
        }
        public double AverageAtmosphereScore
        {
            get
            {
                return (Ratings.Count > 0) ?Ratings.Select(r => r.AtmosphereScore).Sum() / Ratings.Count:0;
            }
        }
        public double Score
        {
            get
            {
                return (AverageFoodScore + AverageCleanlinessScore + AverageAtmosphereScore) / 3;
            }
        }
    }
}