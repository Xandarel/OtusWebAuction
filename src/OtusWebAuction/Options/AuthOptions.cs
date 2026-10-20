using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace OtusWebAuction.Options
{
    public sealed class AuthOptions
    {
        public const string ISSUER = "WebAuthAuctionBackend"; // издатель токена
        public const string AUDIENCE = "WebAuthAuctionClient"; // потребитель токена

        //TODO: Вынести в секрет
        const string _key = "mysupersecret_secretsecretsecretkey!123";   // ключ для шифрации
        public static SymmetricSecurityKey GetSymmetricSecurityKey() =>
            new(Encoding.UTF8.GetBytes(_key));
    }
}
