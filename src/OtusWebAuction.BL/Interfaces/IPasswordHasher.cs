namespace OtusWebAuction.BL.Interfaces
{
    public interface IPasswordHasher
    {
        string Hash(string email, string password);
        bool Verify(string email, string password, string passwordHash);
    }
}
