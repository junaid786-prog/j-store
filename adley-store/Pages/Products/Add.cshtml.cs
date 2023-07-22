using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using adley_store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace adley_store.Pages.Products
{
	public class AddModel : PageModel
    {
        private readonly AdleyDBContext dbContext;
        [BindProperty]
        public AddProduct AddProductRequest { get; set; }

        public AddModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
        }


        public IActionResult OnGet()
        {
            var a = HttpContext.Session.GetString("userId");
            if (a == null) {
                return RedirectToPage("/Products/List");
            }
            else {
                return Page();
            }

        }
        public IActionResult OnPost()
        {
            var a = HttpContext.Session.GetString("userId");
            if (a == null)
            {
                return RedirectToPage("Account/Login");
            }

            try {
                int Uid = int.Parse(a);
                var product = new Product()
                {
                    Name = AddProductRequest.Name,
                    Description = AddProductRequest.Description,
                    Price = AddProductRequest.Price,
                    Quantity = AddProductRequest.Quantity,
                    BannerUrl = AddProductRequest.BannerUrl ?? "https://imgv3.fotor.com/images/gallery/AI-3D-Female-Profile-Picture.jpg",
                    UserId = Uid,
                    Id = AddProductRequest.Id
                };
                dbContext.Products.Add(product);
                dbContext.SaveChanges();
                return RedirectToPage("/Products/List");
            } catch (Exception e) {
                TempData["ErrorMessage"] = e.Message;
                return RedirectToPage("/Products/Add");
            }
        }
    }
}
