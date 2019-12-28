using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BethanysPies.Models
{
    public class MockPieRepository : IPieRepository
    {
        private readonly ICategoryRepository _categoryRepository = new MockCategoryRepository();

        public IEnumerable<Pie> AllPies =>
            new List<Pie>
            {
                new Pie {PieId = 1, Name = "Strawberry Pie", Price = 15.95M, ShortDescription = "Fresh Strawberries", LongDescription = "Baked from our in house strawberries and made with love.", AllergyInfo = "Contains milk, eggs, and flour", Category = _categoryRepository.AllCategories.ToList()[0], ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypie.jpg", InStock=true, IsPieOfTheWeek=false, ImgThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/strawberrypiesmall.jpg"},

                new Pie {PieId = 2, Name = "Cheese Cake", Price = 18.95M, ShortDescription = "Creamy Cheesecake", LongDescription = "Rich, sweet, creamy cheesecake sure to fill up that hunger hole", AllergyInfo = "Contains milk, eggs, and flour", Category = _categoryRepository.AllCategories.ToList()[1], ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecake.jpg", InStock=true, IsPieOfTheWeek=false, ImgThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/cheesecakesmall.jpg"},

                new Pie {PieId = 3, Name = "Rhubarb Pie", Price = 15.95M, ShortDescription = "Southern Rhubarb Pie", LongDescription = "Our southern rhubarb pie tastes like the heart of the south, with a recipe dating from 1892 from my own great-grandmother.", AllergyInfo = "Contains Milk and eggs", Category = _categoryRepository.AllCategories.ToList()[0], ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpie.jpg", InStock=true, IsPieOfTheWeek=true, ImgThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/rhubarbpiesmall.jpg"},

                new Pie {PieId = 4, Name = "Pumpkin Pie", Price = 12.95M, ShortDescription = "Pumpkin Fall Flavors", LongDescription = "Celebrate fall with our pumpkin pie sure to warm you up on a cold a fall night.", Category = _categoryRepository.AllCategories.ToList()[2], ImageUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpie.jpg", InStock=true, IsPieOfTheWeek=true, ImgThumbnailUrl="https://gillcleerenpluralsight.blob.core.windows.net/files/pumpkinpiesmall.jpg"}
            };
    
        public IEnumerable<Pie> PiesOfTheWeek { get; }

        public Pie GetPieById(int PieId) => AllPies.FirstOrDefault(p => p.PieId == PieId);
      
    }
}
