using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;

namespace API.Services;

public class UserSeedService
{
    /// <summary>
    /// Generates 100 test users with sequential usernames and passwords, serializes them to JSON, and writes the result to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path where the generated user data will be saved as JSON.</param>
    public void WriteTestUsers(string filePath)
    {
        var users = new List<AppUser>();

        for (int i = 1; i <= 100; i++)
        {
            var userName = $"testuser{i}";
            var password = $"Password{i}";
            CreatePasswordHash(password, out byte[] hash, out byte[] salt);

            users.Add(new AppUser
            {
                Id = i,
                UserName = userName,
                PasswordHash = hash,
                PassworSalt = salt
            });
        }

        var json = JsonSerializer.Serialize(users, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText(filePath, json);
    }

    /// <summary>
    /// Generates a cryptographic hash and salt for the specified password using HMACSHA512.
    /// </summary>
    /// <param name="password">The plaintext password to hash.</param>
    /// <param name="passwordHash">The resulting password hash as a byte array.</param>
    /// <param name="passwordSalt">The generated salt as a byte array.</param>
    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}