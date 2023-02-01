using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Models.Restaurant;
using RestaurantRaterMVC.Services.Restaurant;

namespace RestaurantRaterMVC.Controllers
{
    [Route("[controller]")]
    public class RestaurantController : Controller
    {
        private IRestaurantService _service;

        public RestaurantController(IRestaurantService service)
        {
            _service = service;
        }
    
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<RestaurantListItem> restaurants = await _service.GetAllRestaurants();
            return View(restaurants);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        [HttpGet]
        [ActionName("Details")]
        public async Task<IActionResult> Restaurant(int id)
        {
            return View(await _service.GetRestaurantById(id));
        }
        [HttpGet]
        [Route("Create")]
        public async Task<IActionResult> RestaurantCreate()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> RestaurantCreate(RestaurantCreate model)
        {
            if (!ModelState.IsValid)
                return View(model);
            
            await _service.CreateRestaurant(model);
                return RedirectToAction(nameof(Index));

        }

        [HttpGet("Edit/{id}")]
        public async Task<IActionResult> RestaurantEdit(int id)
        {
            var restaurant = await _service.GetRestaurantById(id);

        RestaurantEdit restaurantEdit = new RestaurantEdit()
        {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location,
        };

            return View(restaurantEdit);
        }

        [HttpPost("Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestaurantEdit(int id, RestaurantEdit model)
        {
            if (!ModelState.IsValid)
            {
                return View(ModelState);
            }

        if (await _service.UpdateRestaurant(model))
            return RedirectToAction("Details", new { id = id });
            else 
            return RedirectToAction(nameof(Error));
        }

        [HttpGet("Delete/{id}")]
        public async Task<IActionResult> RestaurantDelete(int? id)
        {
            var restaurant = await _service.GetRestaurantById(id.Value);

        var restaurantDetail = new RestaurantDetail
            {
            Id = restaurant.Id,
            Name = restaurant.Name,
            Location = restaurant.Location,
        };

        return View(restaurantDetail);
        }

        [ HttpPost("Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RestaurantDelete(int id)
        {
            if (await _service.DeleteRestaurant(id))
            return RedirectToAction(nameof(Index));
            else
            return RedirectToAction(nameof(Error));
        }
    }
}