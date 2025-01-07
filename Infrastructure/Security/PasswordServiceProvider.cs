using Core.Contracts.Providers;

namespace Infrastructure.Security;

public class PasswordServiceProvider:IPasswordServiceProvider
{
    public string HashPassword(string password)
    {
        try
        {
            BCrypt.Net.BCrypt.InterrogateHash(password);
        }
        catch (BCrypt.Net.HashInformationException e)
        {
            // Generate a salt and hash the password
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
            return hashedPassword;
            
        }
        return password;
    }

    public bool VerifyPassword(string password, string hashedPassword)
    {
        
        // Verify the password against the hashed password
        bool isValid = BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        return isValid;
    }
}