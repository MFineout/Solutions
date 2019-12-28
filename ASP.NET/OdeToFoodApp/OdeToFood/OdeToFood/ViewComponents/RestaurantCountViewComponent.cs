using Microsoft.AspNetCore.Mvc;
using OdeToFood.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OdeToFood.ViewComponents
{
    //ViewComponent does not respond to HTTP request (No 'GET' or 'POST')
    public class RestaurantCountViewComponent
        : ViewComponent
    {
        private readonly IRestaurantData restaurantData;

        public RestaurantCountViewComponent(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        public IViewComponentResult Invoke() //controller
        {
            var count = restaurantData.GetCountOfRestaurants(); //count is model
            return View(count); //passing model to view, displays count
        }
    }
}
