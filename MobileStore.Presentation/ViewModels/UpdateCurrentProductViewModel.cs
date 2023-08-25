namespace MobileStore.Presentation.ViewModels
{
    public class UpdateCurrentProductViewModel
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }
        public string Img { get; init; } = null!;
    }
}
