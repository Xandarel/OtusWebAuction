namespace OtusWebAuction.BL.Catrgory.Create
{
    public sealed record CategoryRequest
    {
        public required string Name { get; set; }

        public int? ParentId { get; set; }

        public IReadOnlyCollection<string>? Subcategories { get; set; }
    }
}
