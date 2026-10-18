namespace OtusWebAuction.BL.Users.Register
{
    /// <summary>
    /// Запрос для регистрации пользователя
    /// </summary>
    public sealed record RegisterUserRequest
    {
        /// <summary>
        /// Имя
        /// </summary>
        public required string Name { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public required string Surname { get; set; }

        /// <summary>
        /// Адресс электронной почты
        /// </summary>
        public required string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        public required string Password { get; set; }
    }
}
