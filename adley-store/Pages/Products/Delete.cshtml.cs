using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using adley_store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace adley_store.Pages.Products
{
	public class DeleteModel : PageModel
    {
        private readonly AdleyDBContext dbContext;
        public int CurrentUser { get; set; }
        public Product SingleProduct { get; set; }

        [BindProperty]
        public AddProduct AddProductRequest { get; set; }

        public DeleteModel(AdleyDBContext adleyDBContext)
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
            try
            {
                CurrentUser = int.Parse(currentUser);
            }
            catch(Exception e) {
                return RedirectToPage("/Account/Login");
            }

            

            if (int.TryParse(Request.Query["id"], out int id))
            {
                // Use the 'id' value to retrieve the product from the database or perform any other actions
                var a = dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (a == null)
                {
                    TempData["ErrorMessage"] = "Product Not Found With This ID";
                    return Page();
                }
                else SingleProduct = a;
                if (a.UserId != CurrentUser) {
                    TempData["ErrorMessage"] = "You Are Not Authorized To Do This";
                    return Page();
                }
                else {
                    dbContext.Products.Remove(a);
                    dbContext.SaveChanges();
                    return RedirectToPage("/Products/List");
                }
            }

            else
            {
                return RedirectToPage("/Products/List");
            }
        }
    }
}
