using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantRaterMVC.Models.Rating
{
    public class RatingCreate
    {
        [Required]
        [Display(Name = "Restaurant")]
        public int RestaurantId { get; set; }
        [Required]
        public double FoodScore { get; set; }
        [Required]
        public double CleanlinessScore { get; set; }
        [Required]
        public double AtmosphereScore { get; set; }
        public IEnumerable<SelectListItem> RestaurantOptions { get; set; } = new List<SelectListItem>();
    }
}