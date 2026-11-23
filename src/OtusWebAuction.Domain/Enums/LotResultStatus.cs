namespace OtusWebAuction.Domain.Enums
{
    // Статус результата операции с лотом
    public enum LotResultStatus
    {
        Created = 0,
        Updated = 1,
        NotFound = 2,
        NotOwner = 3,
        InvalidData = 4,
        Deleted = 5,
        SentToModerate = 6,
        Found = 7,
        Rejected = 8,
        Approved = 9
    }
}
