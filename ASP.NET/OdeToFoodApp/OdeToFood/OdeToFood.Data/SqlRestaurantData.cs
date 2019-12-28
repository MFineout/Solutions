using Microsoft.EntityFrameworkCore;
using OdeToFood.Core;
using System.Collections.Generic;
using System.Linq;

namespace OdeToFood.Data
{
    public class SqlRestaurantData : IRestaurantData
    {
        private readonly OdeToFoodDbContext db;

        public SqlRestaurantData(OdeToFoodDbContext db)
        {
            this.db = db;
        }

        public Restaurant Add(Restaurant newRestaurant)
        {
            db.Add(newRestaurant);
            return newRestaurant;

        }

        public int Commit()
        {
            //NOTE: nothing is changed until Commit is invoked
            return db.SaveChanges(); //returns an int that represents # of rows affected in the db
        }

        public Restaurant Delete(int id)
        {
            var restaurant = GetById(id);
            if(restaurant != null)
            {
                db.Remove(restaurant); //same as db.Restaurants.Remove(restaurant)
            }
            return restaurant;
        }

        public Restaurant GetById(int id)
        {
            return db.Restaurants.Find(id);
        }

        public int GetCountOfRestaurants()
        {
            return db.Restaurants.Count();
        }

        public IEnumerable<Restaurant> GetRestaurantsByName(string name)
        {
            var query = from r in db.Restaurants
                        where r.Name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.Name
                        select r;
            return query;
        }

        public Restaurant Update(Restaurant updatedRestaurant)
        {
            var entity = db.Restaurants.Attach(updatedRestaurant); //Begins tracking the entity changes
            entity.State = EntityState.Modified; //tells entity framework that this entity is modified
            return updatedRestaurant;
        }
    }
}
