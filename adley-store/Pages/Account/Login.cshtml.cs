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

namespace adley_store.Pages.Account
{
	public class LoginModel : PageModel
    {
        [BindProperty]
        public string Email { get; set; }
        [BindProperty]
        public string Password { get; set; }

        private readonly AdleyDBContext dbContext;
        public LoginModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
        }

        public void OnGet()
        {
        }
        public IActionResult OnPost()
        {
            Console.WriteLine("\n\n User: " + Email + " " + Password);
            var user = dbContext.Users.FirstOrDefault(u => u.Email == Email && u.Password == Password);

            if (user != null)
            {
                // Authentication successful
                // You can store authentication information in the user's session or cookie
                HttpContext.Session.SetString("userId", user.Id.ToString());
                // Redirect to a protected page or dashboard
                return RedirectToPage("/Products/List");
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid username or password";
                return RedirectToPage("/Account/Login");
            }

        }
    }
}
