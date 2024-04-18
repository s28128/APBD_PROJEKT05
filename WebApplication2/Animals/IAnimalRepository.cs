using System.Collections.Generic;
namespace WebApplication2.Animals;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAllAnimals(string orderBy);
    bool AddAnimal(Animal animal);
    bool UpdateAnimal(int id, Animal animal);
    bool DeleteAnimal(int id);
}