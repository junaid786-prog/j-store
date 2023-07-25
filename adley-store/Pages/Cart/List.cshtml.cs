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
        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetString("userId");
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            Products = new List<Product>();
            // first get user cart
            var userCart = dbContext.Cart.FirstOrDefault(c => c.UserId == int.Parse(currentUser));
            if (userCart == null) return Page();

            CartItems = dbContext.CartItems
                .Where(cItem => cItem.CartId == userCart.Id)
                .ToList();
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
            return Page();
        }
    }
}
