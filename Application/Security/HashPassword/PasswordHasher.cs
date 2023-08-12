using Application.Common.Security.HashPassword;
using BCryptNet = BCrypt.Net.BCrypt;

namespace Application.Security.HashPassword;

public class PasswordHasher : IPasswordHasher
{
    private const int key = 12;
    public string HashPassword(string password)
    {
        string salt = BCryptNet.GenerateSalt(key);
        string hash= BCryptNet.HashPassword(password, salt);
        return hash;
    }

    public bool VerifyPasswordB(string password, string hashedPassword)
    {
        return BCryptNet.Verify(password, hashedPassword);
    }
}