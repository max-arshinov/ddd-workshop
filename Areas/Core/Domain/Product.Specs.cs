using Force.Ddd;

namespace DddWorkshop.Areas.Core.Domain
{
    public class ProductSpecs
    {
        internal ProductSpecs()
        {
        }

        public Spec<Product> IsForSale { get; } = new Spec<Product>(x => x.Price > 0);
    }
}