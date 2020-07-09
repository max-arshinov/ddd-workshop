namespace DddWorkshop.Areas.Shop.Domain
{
    public interface ICartStorage
    {
        Cart Cart { get; }
        void SaveChanges();
    }
}