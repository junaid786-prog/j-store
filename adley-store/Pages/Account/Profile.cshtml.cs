using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace adley_store.Pages.Account
{
	public class ProfileModel : PageModel
    {
        private readonly AdleyDBContext dbContext;

        public List<Product> Products { get; set; }
        public UserProfile UserInfo { get; set; }
        public List<CartItem> CartItems { get; set; }
        public List<Product> CartProducts { get; set; }

        public ProfileModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
            Products = new List<Product>();
            CartProducts = new List<Product>();
            UserInfo = new UserProfile();
            CartItems = new List<CartItem>();
        }

        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetString("userId");
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }


            Products = dbContext.Products.Where(p => p.UserId == int.Parse(currentUser)).ToList();
            var user = dbContext.Users.FirstOrDefault(user => user.Id == int.Parse(currentUser));
            if (user == null)
            {
                return RedirectToPage("/Account/Login");
            }
            UserInfo = user;
            var userCart = dbContext.Cart.FirstOrDefault(c => c.UserId == int.Parse(currentUser));
            if (userCart == null) return Page();

            CartItems = dbContext.CartItems
                .Where(cItem => cItem.CartId == userCart.Id)
                .ToList();
            foreach (CartItem cart in CartItems)
            {
                var product = dbContext.Products.FirstOrDefault(p => p.Id == cart.ProductId);
                if (product != null) CartProducts.Add(product);
            }

            return Page();
        }
    }
}
