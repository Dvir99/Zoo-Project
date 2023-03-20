using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ZooProject.Models;
using ZooProject.Repositories;

namespace ZooProject.Controllers
{
    public class ManagementController : Controller
    {
        IRepository _repos;
        ILogger<ManagementController> _logger;
        public ManagementController(IRepository repos, ILogger<ManagementController> logger)
        {
            _repos = repos;
            _logger = logger;
        }

        #region Display Actions
        public IActionResult DisplayAnimals()
        {
            var animals = _repos.GetAnimals();
            _logger.LogInformation("Admin entered Catalog page");
            return View(animals);
        }
        public IActionResult FilterByCategory(string categoryName)
        {
            if (categoryName == "All")
            {
                return RedirectToAction("DisplayAnimals");
            }
            var animals = _repos.GetAnimalsByCategory(categoryName);
            _logger.LogInformation($"Admin used filter: {categoryName}");
            return View("DisplayAnimals", animals);
        }
        #endregion

        #region Create Actions
        public IActionResult CreateScreen()
        {
            var animal = new Animal();
            _logger.LogInformation("User entered Create page");
            return View(animal);
        }
        public IActionResult CreateAnimal(string name, int age, string description, string? imgSource = "", int categoryId = 1)
        {
            //if (imgSource.Length < 40)
            //{
            //    imgSource = "~/images/notfound.jpg";
            //}
            if (ModelState.IsValid)
            {
                _repos.CreateNewAnimal(name, age, description, imgSource, categoryId);
                _logger.LogInformation($"User created new animal: {name}");
                return RedirectToAction("DisplayAnimals");
            }
            return RedirectToAction("CreateScreen");
        }

        #endregion

        #region Edit Actions
        public IActionResult EditScreen(int id)
        {
            var animalToEdit = _repos.GetAnimalById(id);
            _logger.LogInformation("User entered Edit page");
            return View(animalToEdit);
        }
        public IActionResult EditAnimal(int id, string name, int age, string description, string? imgSource = "~/images/notfound.jpg", int categoryId = 1)
        {
            var animal = _repos.GetAnimalById(id);
            if(imgSource.Length < 40)
            {
                imgSource = "~/images/notfound.jpg";
            }
            if (ModelState.IsValid)
            {
                _repos.EditAnimal(id, name, age, description, imgSource, categoryId);
                _logger.LogInformation($"User edited an animal: {name}");
                return RedirectToAction("DisplayAnimals");
            }
            return View("EditScreen", animal);
        }

        #endregion

        #region Delete Action
        public IActionResult DeleteAnimal(int id)
        {
            var animal = _repos.GetAnimalById(id);
            _repos.DeleteAnimal(id);
            _logger.LogInformation($"User deleted an animal: {animal.Name}");
            return RedirectToAction("DisplayAnimals");
        }
        #endregion



    }
}
