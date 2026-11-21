namespace OtusWebAuction.BL.Lots.Request
{
    public sealed record CreateLotRequest
    {
        /// <summary>Наименование лота</summary>
        public required string Name { get; set; }

        /// <summary>Описание лота</summary>
        public string? Description { get; set; }

        /// <summary>Категория лота</summary>
        public required int CategoryId { get; set; }

        /// <summary>Стартовая цена лота</summary>
        public required decimal StartPrice { get; set; }

        /// <summary>Цена немедленного выкупа</summary>
        public decimal? BuyoutPrice { get; set; }

        /// <summary>Минимальное повышение ставки</summary>
        public required decimal MinStep { get; set; }
    }
}
