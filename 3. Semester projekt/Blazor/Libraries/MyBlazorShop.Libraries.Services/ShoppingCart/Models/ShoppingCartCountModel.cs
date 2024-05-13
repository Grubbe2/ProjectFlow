using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBlazorShop.Libraries.Services.ShoppingCart.Models
{

// Denne klasse gemmes som en cascading værdi, så child komponents kan bruge parametrene. MainLayout.Razor er det noteret
    public class ShoppingCartCountModel
    {
        public int Count { get; set; }

        // Når en Item tiløjes/slettes til kurven 
        public event Action? CountChange;

        // Når OnCountChange() kaldes opdateres kurven
        public void OnCountChange()
        {
            CountChange?.Invoke();
        }
    }
}
