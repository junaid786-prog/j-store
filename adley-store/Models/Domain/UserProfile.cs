using System;
namespace adley_store.Models.Domain
{
	public class UserProfile
	{
		public int Id { get; set; }
		public string Name { get; set; }
		public string Email { get; set; }
		public string Password { get; set; }
		public string Address { get; set; }
        public string ProfileUrl { get; set; }

	}
}

