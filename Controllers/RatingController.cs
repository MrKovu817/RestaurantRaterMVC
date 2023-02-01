using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using RestaurantRaterMVC.Models;
using RestaurantRaterMVC.Models.Rating;
using RestaurantRaterMVC.Services.Rating;

namespace RestaurantRaterMVC.Controllers
{
    [Route("[controller]")]
    public class RatingController : Controller
    {
        private IRatingService _service;
        public RatingController(IRatingService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetRatings());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }

        // public async Task<IActionResult> Display(int id)
        // {
        
        // }
    [HttpGet]
    [Route("Create")]
        public async Task<IActionResult> Create()
        {
            RatingCreate rC = new();
            var selectListItem = await _service.SelectListItem();
            rC.RestaurantOptions = selectListItem;

            return View(rC);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Route("Create")]
        public async Task<IActionResult> Create(Models.Rating.RatingCreate model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            if (await _service.CreateRating(model))
                return RedirectToAction(nameof(Index));
                else
                return RedirectToAction(nameof(Error));
                
                
        }
    }
}