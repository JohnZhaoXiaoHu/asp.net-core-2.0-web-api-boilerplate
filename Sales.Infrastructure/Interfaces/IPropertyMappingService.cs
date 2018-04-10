namespace Sales.Infrastructure.Interfaces
{
    public interface IPropertyMappingService
    {
        bool ValidMappingExistsFor(string fields);
    }
}