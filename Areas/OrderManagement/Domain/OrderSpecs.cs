using System.Security.Principal;
using Force.Ddd;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class OrderSpecs
    {
        internal OrderSpecs()
        {
        }
        
        public Spec<Order> ByIdentity(IIdentity identity) => 
            new Spec<Order>(x => x.User.UserName == identity.Name);
    }
}