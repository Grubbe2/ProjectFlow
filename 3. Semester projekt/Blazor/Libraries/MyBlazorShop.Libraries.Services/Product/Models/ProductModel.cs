using System.Security.AccessControl;

namespace MyBlazorShop.Libraries.Services.Product.Models
{
    /// <summary>
    /// Stores a product.
    /// </summary>
    public class ProductModel
    {
        /// <summary>
        /// Unique identifier of the product.
        /// </summary>

        public string Sku {get ;set;}
        

        public int ID { get; set; }
        public string Name { get; set; }
        public double Length { get; set; }
        public double Width { get; set; }
        public double Thickness { get; set; }
        public double Volume { get; set; }
        public decimal Price { get; set; }
        public string Type { get; set; }
        public string? Equipment { get; set; }
        public string Image { get; set; }
        public byte[] RowVersion { get; set; }
        public bool IsRented { get; set; }



        /// <summary>
        /// The route slug of the product.
        /// </summary>
        public string Slug
        {
            get
            {
                return Sku.ToLower();
            }
        }

        /// <summary>
        /// The full URL of the product
        /// </summary>
        public string FullUrl
        {
            get
            {
                return string.Format("/product/{0}", Slug);
            }
        }

        /// <summary>
        /// Constructs a new product.
        /// </summary>
        /// <param name="sku">Unique identifier of the product.</param>
        /// <param name="name">Name of the product.</param>
        /// <param name="price">Price of the product.</param>
        /// <param name="image">Image path of the product.</param>
        public ProductModel(string name, double length, double width, double thickness, double volume, string type, string equipment, decimal price, string image)
        {
            
            Name = name;
            Length = length;
            Width = width;
            Thickness = thickness;
            Volume = volume;
            Type = type;
            Equipment = equipment;
            Price = price;
            Image = image;
        }
    }
}
