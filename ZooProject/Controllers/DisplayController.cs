using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using ZooProject.Models;
using ZooProject.Repositories;

namespace ZooProject.Controllers
{
    public class DisplayController : Controller
    {
        IRepository _repos;
        ILogger<DisplayController> _logger;
        public DisplayController(IRepository repos, ILogger<DisplayController> logger)
        {
            _repos = repos;
            _logger = logger;
        }
        #region Display Group Of Animals
        public IActionResult DisplayAll()
        {
            var animals = _repos.GetAnimals();
            _logger.LogInformation("User entered Catalog page");
            return View(animals);
        }
        public IActionResult DisplayTop2()
        {
            var animals = _repos.GetTop2();
            _logger.LogInformation("User entered Home page");
            return View(animals);
        }
        public IActionResult FilterByCategory(string categoryName)
        {
            if(categoryName == "All")
            {
                return RedirectToAction("DisplayAll");
            }
            var animals = _repos.GetAnimalsByCategory(categoryName);
            _logger.LogInformation($"User used filter: {categoryName}");
            return View("DisplayAll", animals);
        }
        #endregion

        #region Display Single Animal
        public IActionResult AnimalDetails(int id)
        {
            var animal = _repos.GetAnimalById(id);
            ViewBag.CategoryName = _repos.GetAnimalCategoryName(id);
            _logger.LogInformation($"User entered Animal Details page: {animal.Name}");
            return View(animal);
        }
        #endregion

        #region Comment Action
        public IActionResult AddComment(int id, string comment)
        {
            if (comment != null)
            {
                _repos.AddCommentOnAnimal(id, comment);
            }
            var animal = _repos.GetAnimalById(id);
            ViewBag.CategoryName = _repos.GetAnimalCategoryName(id);
            _logger.LogInformation($"User commented on Animal: {animal.Name}");
            return View("AnimalDetails", animal);
        }
        #endregion



    }
}