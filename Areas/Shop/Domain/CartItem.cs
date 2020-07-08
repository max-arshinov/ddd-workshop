namespace DddWorkshop.Areas.Shop.Domain
{
    public class CartItem
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public double Price { get; set; }
        
        public int Count { get; set; }

        public override string ToString() => $"{ProductName}: ${Price}";
    }
}