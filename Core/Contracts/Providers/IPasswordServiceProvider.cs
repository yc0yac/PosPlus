namespace Core.Contracts.Providers;

public interface IPasswordServiceProvider
{
    string HashPassword(string password);
    bool VerifyPassword(string password, string hashedPassword);
}