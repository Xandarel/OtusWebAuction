namespace OtusWebAuction.BL.Users.Dto
{
    public sealed record UserDto
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }

        public required Guid Id { get; set; }
    }
}
