namespace OtusWebAuction.BL.Users.Login
{
    public sealed record LoginUserRequest
    {
        public required string Email { get; set; }

        public required string Password { get; set; }
    }
}
