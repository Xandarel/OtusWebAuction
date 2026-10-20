using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.DB.Models;
using Riok.Mapperly.Abstractions;

namespace OtusWebAuction.DB.Mapper
{
    [Mapper]
    public static partial class UserMapper
    {
        [MapperIgnoreTarget(nameof(User.Id))]
        [MapperIgnoreTarget(nameof(User.CreatedDate))]
        [MapperIgnoreTarget(nameof(User.LastUpdatedDate))]
        public static partial User ToUser(RegisterUserRequest request);

        public static partial RegisterUserResponce ToResponse(User user);
    }
}
