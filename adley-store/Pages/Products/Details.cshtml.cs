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
	public class DetailsModel : PageModel
    {
        private readonly AdleyDBContext dbContext;
        public int CurrentUser = -22;
        public Product? SingleProduct { get; set; }

        public DetailsModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
        }
        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetString("userId");

            if (currentUser != null)
            {
                try
                {
                    CurrentUser = int.Parse(currentUser);
                }
                catch (Exception e)
                {
                    CurrentUser = -22;
                }
            }
            

            if (int.TryParse(Request.Query["id"], out int id))
            {
                // Use the 'id' value to retrieve the product from the database or perform any other actions
                SingleProduct = dbContext.Products.FirstOrDefault(p => p.Id == id);


                return Page();
            }
            else
            {
                return RedirectToPage("/Products/List");
            }
        }
    }
}
