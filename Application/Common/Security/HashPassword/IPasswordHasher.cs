namespace Application.Common.Security.HashPassword
{
    public interface IPasswordHasher
    {
        string HashPassword(string password);
        bool VerifyPasswordB(string password, string hashedPassword);
    }
}
