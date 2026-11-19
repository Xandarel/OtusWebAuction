using Microsoft.AspNetCore.Identity;
using OtusWebAuction.BL.Interfaces;

namespace OtusWebAuction.Security
{
    public class UserPasswordHasher : IPasswordHasher
    {
        private readonly PasswordHasher<string> _hasher = new();
        public string Hash(string email, string password) => _hasher.HashPassword(email, password);
        public bool Verify(string email, string password, string passwordHash)
        {
            return _hasher.VerifyHashedPassword(email, passwordHash, password)
                is PasswordVerificationResult.Success
                or PasswordVerificationResult.SuccessRehashNeeded;
        }
    }
}
