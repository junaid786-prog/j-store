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
	public class AddModel : PageModel
    {
        private readonly AdleyDBContext dbContext;

        public adley_store.Models.Domain.Cart UserCart { get; set; }

        public AddModel(AdleyDBContext adleyDBContext)
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

            // get product id from url
            if (int.TryParse(Request.Query["id"], out int productId))
            {
                int userId = int.Parse(currentUser);
                var existedCart = dbContext.Cart.FirstOrDefault(p => p.UserId == int.Parse(currentUser));
                Console.WriteLine("existed cart" + existedCart);

                if (existedCart == null) {
                    Console.WriteLine("not exit already");
                    UserCart = new adley_store.Models.Domain.Cart()
                    {
                        UserId = userId,
                        Id = userId
                    };
                    dbContext.Cart.Add(UserCart);
                    dbContext.SaveChanges();
                }
                else {
                    UserCart = existedCart;
                }

                Console.WriteLine(productId + " " + UserCart.UserId);
                var isItemAlreadyExists = dbContext.CartItems.FirstOrDefault(ci => ci.ProductId == productId && ci.CartId == UserCart.Id);
                Console.WriteLine("is\n\n");
                Console.WriteLine(isItemAlreadyExists);

                if (isItemAlreadyExists != null) {
                    TempData["ErrorMessage"] = "Item already exists";
                    return RedirectToPage("/Cart/List");
                }

                int idToSave = (int) DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                CartItem cartItem = new CartItem()
                {
                    Id = idToSave,
                    CartId = UserCart.Id,
                    ProductId = productId
                };

                dbContext.CartItems.Add(cartItem);
                dbContext.SaveChanges();

            }

            return RedirectToPage("/Cart/List");
        }
        public void OnPost() {
        }

    }
}
