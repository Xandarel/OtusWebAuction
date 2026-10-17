using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace OtusWebAuction.Options
{
    public sealed class AuthOptions
    {
        public const string ISSUER = "WebAuthAuctionBackend"; // издатель токена
        public const string AUDIENCE = "WebAuthAuctionClient"; // потребитель токена
        
        //TODO: Вынести в секрет
        const string KEY = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(KEY));
    }
}
