namespace MobileStore.Presentation.Mvc.ViewModels
{
    /// <summary>
    /// To interact with Products/Edit
    /// </summary>
    public class EditProductViewModel
    {
        public Guid Id { get; init; }
        public Guid ProductTypeId { get; init; }
        public string Name { get; init; } = null!;
        public string Company { get; init; } = null!;
        public double Price { get; init; }
        public string Img { get; init; } = null!;

        public List<IFormFile> Files { get; set; } = new();
    }
}
