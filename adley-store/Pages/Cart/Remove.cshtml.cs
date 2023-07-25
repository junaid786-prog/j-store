using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace adley_store.Pages.Cart
{
	public class RemoveModel : PageModel
    {
        private readonly AdleyDBContext dbContext;

        public RemoveModel(AdleyDBContext dBContext) {
            this.dbContext = dBContext;
        }

        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetString("userId");
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            // get product id from url
            if (int.TryParse(Request.Query["id"], out int productId)) {
                // find user cart id
                var userCart = dbContext.Cart.FirstOrDefault(c => c.UserId == int.Parse(currentUser));
                if (userCart == null) return Page();

                CartItem? itemToRemove = dbContext.CartItems.FirstOrDefault(item => item.CartId == userCart.Id && item.ProductId == productId);
                if (itemToRemove != null) {
                    dbContext.CartItems.Remove(itemToRemove);
                    dbContext.SaveChanges();
                }
            }
            return RedirectToPage("/Cart/List");
        }
    }
}
