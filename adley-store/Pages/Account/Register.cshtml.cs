using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using adley_store.Data;
using adley_store.Models.Domain;
using adley_store.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace adley_store.Pages.Account
{
	public class RegisterModel : PageModel
    {
        private readonly AdleyDBContext dbContext;
        [BindProperty]
        public RegisterUser RegisterUserRequest { get; set; }

        public RegisterModel(AdleyDBContext adleyDBContext) {
           this.dbContext = adleyDBContext;
        }
        public void OnGet()
        {
        }
        public IActionResult OnPost() {
            int idToSave = (int)DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            var user = new UserProfile() {
                Name = RegisterUserRequest.Name,
                Email = RegisterUserRequest.Email,
                Password = RegisterUserRequest.Password,
                Address = RegisterUserRequest.Address,
                ProfileUrl = RegisterUserRequest.ProfileUrl ?? "https://imgv3.fotor.com/images/gallery/AI-3D-Female-Profile-Picture.jpg",
                Id = idToSave
            };
            HttpContext.Session.SetString("userId", user.Id.ToString());
            dbContext.Users.Add(user);
            dbContext.SaveChanges();
            return RedirectToPage("/Products/List");
        }
        
    }
}
