using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Xml.Linq;

namespace DragoDinde_MudBlazor.Repositories
{

    public class DragodindeRepository
    {
        private readonly string _connectionString;

        public DragodindeRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }

        public void SaveDradoginde(string name, string geneticCode, string username)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
IF NOT EXISTS (SELECT 1 FROM Dradogindes WHERE Name = @Name AND Username = @Username)
BEGIN
    INSERT INTO Dradogindes (Name, GeneticCode, Username)
    VALUES (@Name, @GeneticCode, @Username);
END", connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@GeneticCode", geneticCode);
                command.Parameters.AddWithValue("@Username", username);
                command.ExecuteNonQuery();
            }
        }
        
        public List<(string Name, string GeneticCode)> LoadDragodinde(string username)
        {
            List<(string Name, string GeneticCode)> result = new List<(string Name, string GeneticCode)>();
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT Name, GeneticCode FROM Dradogindes WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                var reader = command.ExecuteReader();
                while(reader.Read())
                {
                    string name = reader.GetString(0);
                    string geneticCode = reader.GetString(1);
                    result.Add(new(name, geneticCode));
                }
            }
            return result;
        }
    }

}
