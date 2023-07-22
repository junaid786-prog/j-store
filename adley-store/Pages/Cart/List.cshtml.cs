using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace adley_store.Pages.Cart
{
	public class ListModel : PageModel
    {
        private readonly AdleyDBContext dbContext;

        public List<CartItem> CartItems { get; set; }
        public List<Product> Products;
        

        public ListModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
        }
        public void OnGet()
        {
            Products = new List<Product>();
            CartItems = dbContext.CartItems.ToList();
            foreach (CartItem cart in CartItems)
            {
                var products = dbContext.Products.Where(p => p.Id == cart.ProductId).ToList();
                if (products.Count > 0)
                {
                    Product product = products[0];
                    Console.WriteLine("\n\n" + cart.ProductId + " p: ");
                    Console.WriteLine("\n\n" + product.Name);
                    Products.Add(product);
                }
            }
        }
    }
}
