using System.Linq;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.Core.Domain
{
    public class UserContext
    {
        private readonly DbContext _dbContext;

        public UserContext(IIdentity identity, DbContext dbContext)
        {
            _dbContext = dbContext;
            Identity = identity;
        }

        private IdentityUser _user;

        public IdentityUser User => Identity.IsAuthenticated 
            ? _user ??= _dbContext 
                .Set<IdentityUser>()
                .FirstOrDefault(x => x.UserName == Identity.Name)
            : null;
        
        public IIdentity Identity { get; protected set; }
    }
}