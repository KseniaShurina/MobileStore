namespace MobileStore.Infrastructure.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public int ProductTypeId { get; set; }
        public ProductType ProductType { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public double Price { get; set; }
        public string Img { get; set; }
    }
}
