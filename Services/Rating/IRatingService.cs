using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Rating
{
    public interface IRatingService
    {
        Task<IEnumerable<SelectListItem>> SelectListItem();
        Task<IEnumerable<RatingListItem>> GetRatings();
        Task<bool> CreateRating(RatingCreate model);
        Task<bool> DeleteRating(int id);
    }
}