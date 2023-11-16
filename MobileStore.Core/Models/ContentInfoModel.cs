namespace MobileStore.Core.Models
{
    public class ContentInfoModel
    {
        public Guid Id { get; init; }
        public string ContentType { get; init; } = null!;
        public string Name { get; init; } = null!;
    }
}
