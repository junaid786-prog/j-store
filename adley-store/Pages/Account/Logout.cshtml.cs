using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace adley_store.Pages.Account
{
	public class LogoutModel : PageModel
    {
        public IActionResult OnGet()
        {
            var currentUser = HttpContext.Session.GetString("userId");
            if (currentUser == null)
            {
                return RedirectToPage("/Account/Login");
            }

            HttpContext.Session.Remove("userId");
            return RedirectToPage("/Account/Login");
        }
    }
}
