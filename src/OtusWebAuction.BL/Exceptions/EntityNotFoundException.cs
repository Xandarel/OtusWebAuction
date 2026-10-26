namespace OtusWebAuction.BL.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName, object key)
            : base($"Сущность '{entityName}' с ключом '{key}' не найдена в базе данных.")
        { }
    }
}
