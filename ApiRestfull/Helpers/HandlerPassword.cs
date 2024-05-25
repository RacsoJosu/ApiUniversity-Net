
namespace ApiRestfull.Helpers
{
    public static class HandlerPassword
    {
        public static string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password, workFactor: 10);
           
        }

        public static bool verifyPassword(string passwordPlain, string hashPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(passwordPlain, hashPassword);
        
        }



    }
}
