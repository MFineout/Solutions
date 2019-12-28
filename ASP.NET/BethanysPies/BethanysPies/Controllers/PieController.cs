using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPies.Models;
using BethanysPies.ViewModels;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace BethanysPies.Controllers
{
    public class PieController : Controller
    {
        // GET: /<controller>/

        private readonly IPieRepository _pieRepository;

        private readonly ICategoryRepository _categoryRepository;

        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository; //set local repository to repository that is being injected.
            _categoryRepository = categoryRepository; // same thing here -- we now have access to the data from the models.
        }

        // action method
        public ViewResult List(string category)
        {
            /*  PiesListViewModel piesListViewModel = new PiesListViewModel();
              piesListViewModel.Pies = _pieRepository.AllPies;
              piesListViewModel.CurrentCategory = "Cheese Cakes"; 

            PiesListViewModel piesListViewModel = new PiesListViewModel()
            {
                Pies = _pieRepository.AllPies,
                CurrentCategory = "Cheese Cakes"
            }; */

            IEnumerable<Pie> pies;
            string currentCategory;

            if(string.IsNullOrEmpty(category))
            {
                pies = _pieRepository.AllPies.OrderBy(p => p.PieId);
                currentCategory = "All pies";
            }
            else
            {
                pies = _pieRepository.AllPies.Where
                        (c => c.Category.CategoryName == category)
                        .OrderBy(p => p.PieId);
                currentCategory = _categoryRepository.AllCategories.FirstOrDefault
                                 (c => c.CategoryName == category)?.CategoryName;
            }

            return View(new PiesListViewModel
            {
                Pies = pies,
                CurrentCategory = currentCategory
            });
        }

        public IActionResult Details(int id)
        {
            var pie = _pieRepository.GetPieById(id);
            if(pie == null)
            {
                return NotFound();
            }
            return View(pie);
        }


    }
}
