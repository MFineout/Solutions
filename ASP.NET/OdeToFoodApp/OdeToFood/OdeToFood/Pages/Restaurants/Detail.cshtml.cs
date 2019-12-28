using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OdeToFood.Core;
using OdeToFood.Data;

namespace OdeToFood
{
    public class DetailModel : PageModel
    {
        private readonly IRestaurantData restaurantData;

        public Restaurant Restaurant { get; set; }

        [TempData]
        public string Message { get; set; }

        public DetailModel(IRestaurantData restaurantData)
        {
            this.restaurantData = restaurantData;
        }

        

        public IActionResult OnGet(int restaurantId)
        {
            Restaurant = restaurantData.GetById(restaurantId); //Restaurant will reference an instance of the Restaurant class with the corresponding Id passed to OnGet. It will reference all of that restaurant's members and thus be able to access those on the cshtml page.
            if(Restaurant == null)
            {
                return RedirectToPage("./NotFound");
            }
            return Page();

        }
    }
}