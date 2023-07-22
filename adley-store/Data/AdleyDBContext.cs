using Microsoft.EntityFrameworkCore;
using adley_store.Models.Domain;

namespace adley_store.Data
{
	public class AdleyDBContext: DbContext
	{
		public AdleyDBContext(DbContextOptions options): base(options)
		{

		}
		public DbSet<UserProfile> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Cart> Cart { get; set; }
		public DbSet<CartItem> CartItems { get; set; }

    }
}

