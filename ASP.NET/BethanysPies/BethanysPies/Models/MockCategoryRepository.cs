using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPies.Models
{
    public class MockCategoryRepository : ICategoryRepository
    {
        public IEnumerable<Category> AllCategories =>
            new List<Category>
            {
                new Category{CategoryId = 1, CategoryName = "Fruit Pies", Description = "All-fruit filling"},
                new Category{CategoryId = 2, CategoryName = "Cheese Cakes", Description = "Cheesy filling"},
                new Category{CategoryId = 3, CategoryName = "Seasonal Pies", Description = "Seasonal fillings"}
            };
    }
}
