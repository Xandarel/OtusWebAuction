using OtusWebAuction.BL.Users.Dto;
using OtusWebAuction.BL.Users.Register;
using OtusWebAuction.BL.Users.Update;
using OtusWebAuction.DB.Models;
using OtusWebAuction.Domain.Enums;
using Riok.Mapperly.Abstractions;

namespace OtusWebAuction.DB.Mapper
{
    [Mapper(AllowNullPropertyAssignment = false)]
    public static partial class UserMapper
    {
        [MapperIgnoreTarget(nameof(User.Id))]
        [MapperIgnoreTarget(nameof(User.CreatedDate))]
        [MapperIgnoreTarget(nameof(User.LastUpdatedDate))]
        [MapperIgnoreTarget(nameof(User.Role))]
        [MapValue(nameof(User.RoleId), 1L)]
        public static partial User ToUser(RegisterUserRequest request);

        [MapperIgnoreTarget(nameof(User.Id))]
        [MapperIgnoreTarget(nameof(User.Email))]
        [MapperIgnoreTarget(nameof(User.Password))]
        [MapperIgnoreTarget(nameof(User.CreatedDate))]
        [MapperIgnoreTarget(nameof(User.LastUpdatedDate))]
        public static partial void Apply(UpdateUserRequest request, User user);

        [MapProperty(nameof(User.Role), nameof(UserDto.Role), Use = nameof(MapRole))]
        public static partial UserDto ToUserDto(User user);

        [MapProperty(nameof(User.Role), nameof(UserDto.Role), Use = nameof(MapRole))]
        public static partial LoginUserDto ToLoginUserDto(User user);

        [UserMapping(Default = false)]
        private static UserRole MapRole(Role? role) =>
            role?.RoleName ?? UserRole.User;
    }
}
