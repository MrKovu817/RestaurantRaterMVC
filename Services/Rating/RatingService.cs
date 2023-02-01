using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Rating;

namespace RestaurantRaterMVC.Services.Rating
{
    public class RatingService : IRatingService
    {
        private readonly RestaurantDbContext _context;

        public RatingService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateRating(RatingCreate model)
        {
            Ratings rating = new Ratings()
            {
                RestaurantId = model.RestaurantId,
                AtmosphereScore = model.AtmosphereScore,
                FoodScore = model.FoodScore,
                CleanlinessScore = model.CleanlinessScore,
            };

            _context.Ratings.Add(rating);
            await _context.SaveChangesAsync();

                return true;
        }

        public Task<bool> DeleteRating(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<RatingListItem>> GetRatings()
        {
            return await _context.Ratings.Include(r => r.Restaurant).Select(r => new RatingListItem{
                RestaurantName = r.Restaurant.Name,
                Score = r.Restaurant.Score

            }).ToListAsync();
        }

        public async Task<IEnumerable<SelectListItem>> SelectListItem()
        {
            return await _context.Restaurants.Select(r => new SelectListItem()
            {
                Text = r.Name,
                Value = r.Id.ToString()
            }).ToListAsync();
        }
    }
}