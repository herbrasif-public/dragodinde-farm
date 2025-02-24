using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;

namespace DragoDinde_MudBlazor.Repositories
{

    public class UserRepository
    {
        public delegate void DelegRefreshNag();
        public static DelegRefreshNag? refreshNavMenu = null;
        public string CurrentUser = null;

        private readonly string _connectionString;

        public UserRepository()
        {
            _connectionString = Environment.GetEnvironmentVariable("ConnectionString");
        }

        public void RegisterUser(string username, string password)
        {
            var passwordHash = HashPassword(password);

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand(@"
IF NOT EXISTS (SELECT 1 FROM Users WHERE Username = @Username)
BEGIN
    INSERT INTO Users (Username, PasswordHash)
    VALUES (@Username, @PasswordHash);
END", connection);
                command.Parameters.AddWithValue("@Username", username);
                command.Parameters.AddWithValue("@PasswordHash", passwordHash);
                command.ExecuteNonQuery();
            }
        }

        public bool ValidateUser(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @Username", connection);
                command.Parameters.AddWithValue("@Username", username);
                var storedHash = (string)command.ExecuteScalar();

                return VerifyPassword(password, storedHash);
            }
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            var hashOfInput = HashPassword(password);
            return hashOfInput == storedHash;
        }
    }

}
