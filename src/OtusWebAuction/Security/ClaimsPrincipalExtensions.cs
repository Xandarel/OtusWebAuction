using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace OtusWebAuction.Security
{
    /// <summary>
    /// Расширения для получения данных текущего пользователя из JWT.
    /// </summary>
    public static class ClaimsPrincipalExtensions
    {
        /// <summary>
        /// Идентификатор текущего пользователя из claim "sub".
        /// Возвращает null, если claim отсутствует или содержит некорректный идентификатор.
        /// </summary>
        public static Guid? GetUserId(this ClaimsPrincipal user) =>
            Guid.TryParse(user.FindFirstValue(JwtRegisteredClaimNames.Sub), out Guid id) ? id : null;
    }
}