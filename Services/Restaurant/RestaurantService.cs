using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Restaurant;

namespace RestaurantRaterMVC.Services.Restaurant
{
    public class RestaurantService: IRestaurantService
    {
        private RestaurantDbContext _context;
        public RestaurantService(RestaurantDbContext context)
        {
            _context = context;
        }

        public async Task<List<RestaurantListItem>> GetAllRestaurants()
        {
            List<RestaurantListItem> restaurants = await _context.Restaurants
            .Include(r => r.Ratings)
            .Select(r => new RestaurantListItem()
            {
                Id = r.Id,
                Name = r.Name,
                Score = r.Score,
            }).ToListAsync();
            return restaurants;
        }

        public async Task<bool> CreateRestaurant(RestaurantCreate model)
        {
            Restaurants restaurant = new Restaurants()
            {
                Name = model.Name,
                Location = model.Location,
            };

            _context.Restaurants.Add(restaurant);
            return await _context.SaveChangesAsync() == 1;
        }

        public async Task<RestaurantDetail> GetRestaurantById(int id)
        {
            Restaurants restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (restaurant is null)
            return null;

            return new RestaurantDetail()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location,
            Score = restaurant.Score,
        };
        }

        public async Task<bool> UpdateRestaurant(RestaurantEdit model)
        {
            Restaurants restaurant = await _context.Restaurants
                .Include(r => r.Ratings)
                .FirstOrDefaultAsync(r => r.Id == model.Id);

            if (restaurant is null)
            return false;
            restaurant.Name = model.Name;
            restaurant.Location = model.Location;
            return await _context.SaveChangesAsync()> 0;
        }

        public Task<bool> DeleteRestaurant(int id)
        {
            throw new NotImplementedException();
        }
    }
}