namespace WebApplication2.Animals
{   
    
    public class AnimalService : IAnimalService
    {
        private readonly IAnimalRepository _animalRepository;

        public AnimalService(IAnimalRepository animalRepository)
        {
            _animalRepository = animalRepository;
        }

        public IEnumerable<Animal> GetAllAnimals(string orderBy)
        {
            return _animalRepository.GetAllAnimals(orderBy);
        }

        public bool AddAnimal(Animal animal)
        {
            return _animalRepository.AddAnimal(animal);
        }

        public bool UpdateAnimal(int id, Animal animal)
        {
            return _animalRepository.UpdateAnimal(id, animal);
        }

        public bool DeleteAnimal(int id)
        {
            return _animalRepository.DeleteAnimal(id);
        }
    }
}