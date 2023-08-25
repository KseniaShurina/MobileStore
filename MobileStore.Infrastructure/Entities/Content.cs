namespace MobileStore.Infrastructure.Entities
{
    public class Content
    {
        public Guid Id { get; init; }
        public string ContentType { get; init; } = null!;
        public string Name { get; init; } = null!;
        public byte[] Data { get; init; } = null!;
    }
}
