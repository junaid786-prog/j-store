namespace adley_store.Models.Domain
{
    public class CartItem
    {
        public int Id { set; get;  }
        public int ProductId { set; get; }
        public int CartId { set; get; }
    }
}