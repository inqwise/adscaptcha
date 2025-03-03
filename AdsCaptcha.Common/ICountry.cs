namespace Inqwise.AdsCaptcha.Common
{
    public interface ICountry
    {
        int Id { get; }
        string Name { get; }
        bool IsDeleted { get; }
        bool IsAccessible { get; }
        string Prefix { get; }
    }
}