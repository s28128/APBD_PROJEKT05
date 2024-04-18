using System.Data.SqlClient;
using Microsoft.Data.SqlClient;

namespace WebApplication2.Animals;

public class AnimalRepository : IAnimalRepository
{
    private readonly IConfiguration _configuration;
    public AnimalRepository(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IEnumerable<Animal> GetAllAnimals(string orderBy)
    {
        var animals = new List<Animal>();

        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            connection.Open();

            var safeOrderBy = new string[] { "Name", "Description", "Category", "Area" }.Contains(orderBy) ? orderBy : "Name";
            using (var command = new SqlCommand($"SELECT IdAnimal, Name, Description, CATEGORY, AREA FROM Animal ORDER BY {safeOrderBy}", connection))
            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    var animal = new Animal
                    {
                        Id = (int)reader["IdAnimal"],
                        Name = reader["Name"].ToString(),
                        Description = reader["Description"]?.ToString(),
                        Category = reader["CATEGORY"].ToString(),
                        Area = reader["AREA"].ToString()
                    };
                    animals.Add(animal);
                }
            }
        }

        return animals;
    }
    public bool AddAnimal(Animal animal)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            connection.Open();

            var query = "INSERT INTO Animal (Name, Description, CATEGORY, AREA) VALUES (@Name, @Description, @Category, @Area)";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                var affectedRows = command.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }
    }
    
    public bool UpdateAnimal(int id, Animal animal)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            connection.Open();

            var query = "UPDATE Animal SET Name = @Name, Description = @Description, CATEGORY = @Category, AREA = @Area WHERE IdAnimal = @Id";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", animal.Name);
                command.Parameters.AddWithValue("@Description", (object)animal.Description ?? DBNull.Value);
                command.Parameters.AddWithValue("@Category", animal.Category);
                command.Parameters.AddWithValue("@Area", animal.Area);

                var affectedRows = command.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }
    }
    public bool DeleteAnimal(int id)
    {
        using (var connection = new SqlConnection(_configuration["ConnectionStrings:DefaultConnection"]))
        {
            connection.Open();

            var query = "DELETE FROM Animal WHERE IdAnimal = @Id";
            using (var command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@Id", id);

                var affectedRows = command.ExecuteNonQuery();
                return affectedRows == 1;
            }
        }
    }

}