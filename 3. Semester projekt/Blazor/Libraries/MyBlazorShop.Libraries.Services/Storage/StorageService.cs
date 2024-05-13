using MyBlazorShop.Libraries.Services.Product.Models;
using MyBlazorShop.Libraries.Services.ShoppingCart.Models;

namespace MyBlazorShop.Libraries.Services.Storage
{
    /// <summary>
    /// Stores the data used for the application.
    /// </summary>
    public class StorageService : IStorageService
    {
        /// <summary>
        /// Stores a list of products.
        /// </summary>
        public IList<ProductModel> Products { get; private set; }

        /// <summary>
        /// Stores the shopping cart.        
        /// </summary>
        public ShoppingCartModel ShoppingCart { get; private set; }

        /// <summary>
        ///  Constructs a storage service.
        /// </summary>
        public StorageService()
        {
            Products = new List<ProductModel>();
            ShoppingCart = new ShoppingCartModel();

            // Store a list of all the products for the online shop.
            //    AddProduct(new ProductModel("The-Minilog", "The MiniLog Surfboard", 6, 21, 2.75, 38.8, "Shortboard", " ", 565, "Surfboard1.jpg"));
            //    AddProduct(new ProductModel("The-Wide-Glider", "The Wide Glider Surfboard", 7.1, 21.75, 2.75, 44.16, "Funboard", " ", 685, "Surfboard2.jpg")); 
            //    AddProduct(new ProductModel("The-Golden-Ration", "The Golden Ratio Surfboard", 6.3, 21.85, 2.9, 43.22, "Funboard", " ", 695, "Surfboard3.jpg")); 
            //    AddProduct(new ProductModel("Mahi-Mahi", "Mahi Mahi Surfboard", 5.4, 20.75, 2.3, 29.39, "Fish", " ", 645, "Surfboard4.jpg"));
            //    AddProduct(new ProductModel("The-Emerald", "The Emerald Surfboard", 9.2, 22.8, 2.8, 65.4, "Longboard", " ", 895, "Surfboard5.jpg"));
            //    AddProduct(new ProductModel("The-Bomb", "The Bomb Surfboard", 5.5, 21, 2.5, 33.7, "Shortboard", " ", 645, "Surfboard6.jpeg"));
            //    AddProduct(new ProductModel("Walden-Magic", "Walden Magic Surfboard", 9.6, 19.4, 3, 80, "Longboard", " ", 1025, "Surfboard7.jpg"));
            //    AddProduct(new ProductModel("Naish-One", "Naish One Surfboard", 12.6, 30, 6, 301, "Sup", "Paddle", 854, "Surfboard8.jpg"));
            //    AddProduct(new ProductModel("Six-Tourer", "Six Tourer Surfboard", 11.6, 32, 6, 270, "Sup", "Fin, Paddle, Pump, Leash", 611, "Surfboard9.jpeg"));
            //    AddProduct(new ProductModel("Naish-Maliko", "Naish Maliko Surfboard", 14, 25, 6, 330, "Sup", "Fin, Paddle, Pump, Leash", 1304, "Surfboard10.jpeg"));
            //
        }

        /// <summary>
        /// Adds a product to the storage.
        /// </summary>
        /// <param name="productModel">The <see cref="ProductModel"/> type to be added.</param>
        public void AddProduct(ProductModel productModel)
        {
            if (!Products.Any(p => p.Sku == productModel.Sku))
            {
                Products.Add(productModel);
            }
        }
    }
}
