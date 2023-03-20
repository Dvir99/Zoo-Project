using NuGet.Packaging;
using System.Xml.Linq;
using ZooProject.Data;
using ZooProject.Models;

namespace ZooProject.Repositories
{
    public class ZooRepository : IRepository
    {
        ZooContext _context;
        public ZooRepository(ZooContext context)
        {
            _context = context;
        }
        #region Get Single Element
        public Animal GetAnimalById(int id)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            if (animal != null)
            {
                var comments = _context.Comments.Where(c => c.AnimalId == animal.AnimalId).ToList();
                animal.Comments.AddRange(comments);
                return animal;
            }
            return null;
        }
        public string GetAnimalCategoryName(int id)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            if (animal != null)
            {
                var categoryName = _context.Categories.Where(c => c.CategoryId == animal.CategoryId).ToList();
                return categoryName[0].CategoryName;
            }
            return null;
        }
        #endregion

        #region Create/Edit/Delete
        public void CreateNewAnimal(string name, int age, string description, string imgSource, int categoryId)
        {
            _context.Animals.Add(new Animal
            {
                Name = name,
                Age = age,
                Description = description,
                ImgSource = imgSource,
                CategoryId = categoryId
            });
            _context.SaveChanges();
        }

        public void DeleteAnimal(int id)
        {
            var animal = _context.Animals.FirstOrDefault(a => a.AnimalId == id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
                _context.SaveChanges();
            }
        }
        public void EditAnimal(int animalId, string name, int age, string description, string imgSource, int categoryId)
        {
            var animal = GetAnimalById(animalId);
            if (animal != null)
            {
                animal.Name = name;
                animal.Age = age;
                animal.Description = description;
                animal.ImgSource = imgSource;
                animal.CategoryId = categoryId;
                _context.SaveChanges();
            }

        }

        #endregion

        #region Lists & Filters
        public ICollection<Animal> GetAnimals()
        {
            return _context.Animals.ToList();
        }
        public ICollection<Animal> GetAnimalsByCategory(string categoryName)
        {
            var category = _context.Categories.First(c => c.CategoryName == categoryName);
            IEnumerable<Animal> query = _context.Animals.Where(a => a.CategoryId == category.CategoryId);
            return query.ToList();
        }
        ICollection<Animal> IRepository.GetTop2()
        {
            var topAnimalsIds = (from a in _context.Animals
                                 join c in _context.Comments on a.AnimalId equals c.AnimalId
                                 group a by a.AnimalId into g
                                 orderby g.Count() descending
                                 select g.Key).Take(2).ToList();
            var animals = new List<Animal>();
            foreach (var id in topAnimalsIds)
            {
                var animal = GetAnimalById(id);
                animals.Add(animal);
            }
            return animals;
        }
        #endregion

        #region Add Comment
        public void AddCommentOnAnimal(int id, string comment)
        {
            var animal = GetAnimalById(id);
            if (animal != null)
            {
                _context.Comments.Add(new Comment { AnimalId = id, CommentString = comment });
                _context.SaveChanges();
            }
        }
        #endregion

       



    }
}
