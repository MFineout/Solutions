using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class ListModel : PageModel
    {
        private readonly IConfiguration config;
        private readonly IRestaurantData restaurantData;
        private readonly ILogger<ListModel> logger;

        public string Message { get; set; }
        public IEnumerable<Restaurant> Restaurants { get; set; }

        [BindProperty(SupportsGet = true)] //tells ASP that this property should recieve information from an HTTP request. -- have to manually set SupportsGet to true, by default this would only receive information on a 'POST'
        public string SearchTerm { get; set; }


        public ListModel(IConfiguration config, 
                         IRestaurantData restaurantData, //razor page now has access to a service that can fetch the restuarant data.
                         ILogger<ListModel> logger) 
        {
            this.config = config;
            this.restaurantData = restaurantData;
            this.logger = logger;
        }

        
        public void OnGet() //responds to HTTP get request --if OnGet does not find a match for searchTerm, it will return null. searchTerm is the name of the input on the ListModel page.
        {
            logger.LogError("Executing ListModel.");   
            Message = config["Message"]; //can access like a dictionary, passing in the config key
            Restaurants = restaurantData.GetRestaurantsByName(SearchTerm);
        }

    }
}