using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace DragoDinde_MudBlazor.Repositories
{

    public class DragodindeRepository
    {
        private readonly string _connectionString;

        public DragodindeRepository(string connectionString)
        {
            _connectionString = connectionString;
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
    }

}
