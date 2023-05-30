using System.Text.Json.Serialization;

namespace Assignment3API.Model
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }

        public int BrandId { get; set; }
        public string BrandName { get; set; } // Add this property
        [JsonIgnore]
        public Brand Brand { get; set; }
     
        public int ProductTypeId { get; set; }
        public string ProductTypeName { get; set; } // Add this property
        [JsonIgnore]
        public ProductType ProductType { get; set; }
    }
}
