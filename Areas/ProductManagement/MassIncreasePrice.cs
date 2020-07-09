namespace DddWorkshop.Areas.ProductManagement
{
    public class MassIncreasePrice
    {
        public MassIncreasePrice(double price)
        {
            Price = price;
        }

        public double Price { get; protected set; }

    }
}