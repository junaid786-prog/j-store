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
	public class ListModel : PageModel
    {
        private readonly AdleyDBContext dbContext;

        public List<Product> Products { get; set; }

        public ListModel(AdleyDBContext adleyDBContext)
        {
            this.dbContext = adleyDBContext;
            Products = new List<Product>();
        }
        public void OnGet()
        {
            Products = dbContext.Products.ToList();
        }
    }
}
