using saltGernerator = BCrypt.Net.BCrypt;

namespace ShopCart.Helpers
{
    public static class PasswordHelper
    {
        public static string EncryptPassword(string password)
        {
            // Generate salt
            string salt = saltGernerator.GenerateSalt();

            string hashedPassword = saltGernerator.HashPassword(password, salt);
            return hashedPassword;
        }

        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // verify password against the hashpassword

            bool checkPassword = saltGernerator.Verify(password, hashedPassword);
            return checkPassword;
        }
    }
}
