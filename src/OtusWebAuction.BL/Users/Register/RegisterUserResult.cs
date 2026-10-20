using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Users.Register
{
    public sealed class RegisterUserResult
    {
        public RegisterUserStatus Status { get; set; }

        public RegisterUserResponce? Responce { get; set; }
        public string? Message { get; set; }
    }
}
