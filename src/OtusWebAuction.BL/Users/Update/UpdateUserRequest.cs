namespace OtusWebAuction.BL.Users.Update
{
    public sealed record UpdateUserRequest
    {

        /// <summary>
        /// Имя
        /// </summary>
        public string? Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string? Surname { get; set; }
    }
}
