using OtusWebAuction.Domain.Enums;

namespace OtusWebAuction.BL.Catrgory.Dto
{
    public class CategoryHandlerDto<T> where T : class
    {
        public HandlerStatus Status { get; set; }

        public T? Responce { get; set; }
        public string? Message { get; set; }
    }
}
