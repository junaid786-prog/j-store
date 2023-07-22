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
	public class UpdateModel : PageModel
    {
        private readonly AdleyDBContext dbContext;
        public int CurrentUser { get; set; }
        public Product SingleProduct { get; set; }

        [BindProperty]
        public AddProduct AddProductRequest { get; set; }

        public UpdateModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
        }
        public IActionResult OnGet()
        {
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

        public IActionResult OnPost()
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
            catch (Exception e)
            {
                return RedirectToPage("/Account/Login");
            }

            if (int.TryParse(Request.Query["id"], out int id))
            {
                // Use the 'id' value to retrieve the product from the database or perform any other actions
                var p = dbContext.Products.FirstOrDefault(p => p.Id == id);
                if (p == null) return RedirectToPage("/Products/List");
                SingleProduct = p;
            }
            else
            {
                return RedirectToPage("/Products/List");
            }
            try
            {
                var product = dbContext.Products.FirstOrDefault(p => p.Id == SingleProduct.Id);

                if (product == null)
                {
                    // Handle case where product is not found
                    return NotFound();
                }
                if (product.UserId != CurrentUser)
                {
                    TempData["ErrorMessage"] = "You Are Not Authorized To Do This";
                    return Page();
                }

                product.Name = AddProductRequest.Name;
                product.Description = AddProductRequest.Description;
                product.Price = AddProductRequest.Price;
                product.Quantity = AddProductRequest.Quantity;
                product.BannerUrl = AddProductRequest.BannerUrl ?? "https://imgv3.fotor.com/images/gallery/AI-3D-Female-Profile-Picture.jpg";

                Console.Write(product.ToString());
                dbContext.SaveChanges();
                return RedirectToPage("/Products/List");
            }
            catch (Exception e)
            {
                Console.WriteLine(SingleProduct.Id);
                TempData["ErrorMessage"] = e.Message;
                Console.WriteLine("\n\n" + SingleProduct.Id + "\n\n");
                string url = "/Products/Update?id=" + SingleProduct.Id.ToString();
                return RedirectToPage(url);
            }
        }
    }
}
