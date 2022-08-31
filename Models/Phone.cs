using System;

namespace MobileStore.Models
{
    public class Phone
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public int Price { get; set; }
        public string Img { get; set; }

        public string GetImageStyle()
        {
            return $"background-image: url({Img}); background-size: cover; width: 100px; height: 100px; ";
        }
    }
}
