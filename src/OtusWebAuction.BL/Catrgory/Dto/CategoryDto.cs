namespace OtusWebAuction.BL.Catrgory.Dto
{
    public sealed record CategoryDto
    {
        public required string Name { get; set; }

        public required int Id { get; set; }
    }
}
