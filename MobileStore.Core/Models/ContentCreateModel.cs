namespace MobileStore.Core.Models
{
    public class ContentCreateModel
    {
        private string ContentType { get; set; } = null!;

        public string Name { get; set; } = null!;

        public Stream Data { get; set; } = null!;
    }
}
