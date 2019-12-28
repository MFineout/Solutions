using OdeToFood.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using static OdeToFood.Core.Restaurant;

namespace OdeToFood.Data
{
    public class InMemoryRestaurantData : IRestaurantData
    {
        readonly List<Restaurant> restaurants;
        public InMemoryRestaurantData()
        {
            restaurants = new List<Restaurant>()
            {
                new Restaurant { Id = 1, Name = "Matt's Pizza", Location = "New York", Cuisine = CuisineType.Italian },
                new Restaurant { Id = 2, Name = "Stacey's Tacos", Location = "New Mexico", Cuisine = CuisineType.Mexican },
                new Restaurant { Id = 3, Name = "Curry Crazy", Location = "Florida", Cuisine = CuisineType.Indian }
            };
        }

        public Restaurant GetById(int id)
        {
            return restaurants.SingleOrDefault(r => r.Id == id); //if r.Id == id return that value from the list of restaurants. This returns the Restaurant instance and all of its members.
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            restaurants.Add(newRestaurant);
            newRestaurant.Id = restaurants.Max(r => r.Id) + 1;
            return newRestaurant;
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name = null)
        {
            return from r in restaurants //return each restaurant and order by name (alphabetically)
                   where string.IsNullOrEmpty(name) || r.Name.StartsWith(name)
                   orderby r.Name
                   select r;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var restaurant = restaurants.SingleOrDefault(r => r.Id == updatedRestaurant.Id);
                if(restaurant != null)
                {
                    restaurant.Name = updatedRestaurant.Name;
                    restaurant.Location = updatedRestaurant.Location;
                    restaurant.Cuisine = updatedRestaurant.Cuisine;
                }
                return restaurant;
        }

        public int Commit() => 0;

        public Restaurant Delete(int id)
        {
            var restaurant = restaurants.FirstOrDefault(r => r.Id == id);
            if(restaurant != null)
            {
                restaurants.Remove(restaurant);
            }
            return restaurant;
        }

        public int GetCountOfRestaurants()
        {
            return restaurants.Count();
        }
    }
}
