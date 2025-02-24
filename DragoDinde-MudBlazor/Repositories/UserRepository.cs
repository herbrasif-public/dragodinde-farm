using System.Collections.Generic;
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

        public string ValidateUser(string username, string password)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                var command = new SqlCommand("SELECT PasswordHash FROM Users WHERE Username = @Username;", connection);
                command.Parameters.AddWithValue("@Username", username);

                var reader = command.ExecuteReader();
                reader.Read();
                var storedHash = reader.GetString(0);

                if (!VerifyPassword(password, storedHash))
                    return "";

                command = new SqlCommand(@"UPDATE Users
SET Token = NEWID(), ExpirationDate = DATEADD(DAY, 5, GETDATE())
WHERE Username = @Username;

SELECT Token FROM Users WHERE Username = @Username;", connection);
                command.Parameters.AddWithValue("@Username", username);

                reader = command.ExecuteReader();
                reader.Read();
                var token = reader.GetGuid(0).ToString();
                return token;
            }
        }

        public void ValidateAndUpdateToken(string token)
        {
            using (SqlConnection connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                string query = "SELECT ExpirationDate, Username FROM Users WHERE Token = @Token";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Token", token);

                var reader = command.ExecuteReader();
                reader.Read();

                object result = reader.GetString(0);
                string userName = reader.GetString(1);

                if (result != null && DateTime.TryParse(result.ToString(), out DateTime expirationDate))
                {
                    if (expirationDate > DateTime.Now)
                    {
                        // Token is valid, update the expiration date to 5 days from now
                        string updateQuery = "UPDATE Users SET ExpirationDate = @NewExpirationDate WHERE Token = @Token";
                        SqlCommand updateCommand = new SqlCommand(updateQuery, connection);
                        updateCommand.Parameters.AddWithValue("@NewExpirationDate", DateTime.Now.AddDays(5));
                        updateCommand.Parameters.AddWithValue("@Token", token);

                        updateCommand.ExecuteNonQuery();
                        CurrentUser = userName;
                    }
                }
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
