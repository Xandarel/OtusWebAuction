namespace OtusWebAuction.BL.Lots.Request
{
    /// <summary>
    /// Запрос на редактирование лота. Задаются только изменяемые поля.
    /// </summary>
    public sealed record UpdateLotRequest
    {
        /// <summary>Наименование лота</summary>
        public string? Name { get; set; }

        /// <summary>Описание лота</summary>
        public string? Description { get; set; }

        /// <summary>Категория лота</summary>
        public int? CategoryId { get; set; }

        /// <summary>Стартовая цена лота</summary>
        public decimal? StartPrice { get; set; }

        /// <summary>Цена немедленного выкупа</summary>
        public decimal? BuyoutPrice { get; set; }

        /// <summary>Минимальное повышение ставки</summary>
        public decimal? MinStep { get; set; }
    }
}
