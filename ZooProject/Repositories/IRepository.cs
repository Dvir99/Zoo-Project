using ZooProject.Models;
namespace ZooProject.Repositories;

public interface IRepository
{
    #region Animal Create/Edit/Delete
    void CreateNewAnimal(string name, int age, string description, string imgSource, int categoryId);
    void DeleteAnimal(int id);
    void EditAnimal(int animalId, string name, int age, string description, string imgSource, int categoryId);
    #endregion

    #region Lists & Filters
    public ICollection<Animal> GetAnimals();
    public ICollection<Animal> GetTop2();
    public ICollection<Animal> GetAnimalsByCategory(string categoryName);

    #endregion

    #region Get Single Element
    Animal GetAnimalById(int id);
    string GetAnimalCategoryName(int id);
    #endregion

    #region Add Comment
    public void AddCommentOnAnimal(int id, string comment);
    #endregion



}
