using Marten.Schema;

namespace ShoppingCart.API.Models
{
    public class Basket
    {
        [Identity]
        public string UserName { get; set; } = default!;

        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Price * x.Quantity);

        public Basket(string userName)
        {
            UserName = userName;
        }

        //For mapping
        public Basket()
        {
        }
    }
}