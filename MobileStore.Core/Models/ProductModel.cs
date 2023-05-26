namespace MobileStore.Core.Models
{
    public class ProductModel
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
    }
}
