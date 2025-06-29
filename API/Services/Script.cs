using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using API.Entities;

namespace API.Services;

public class UserSeedService
{
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

    private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
    {
        using var hmac = new HMACSHA512();
        passwordSalt = hmac.Key;
        passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
    }
}