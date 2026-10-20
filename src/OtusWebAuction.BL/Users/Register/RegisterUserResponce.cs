namespace OtusWebAuction.BL.Users.Register
{
    public sealed record RegisterUserResponce
    {
        public required string Name { get; set; }
        public required string Surname { get; set; }
        public required string Email { get; set; }

        public required Guid Id { get; set; }
    }
}
