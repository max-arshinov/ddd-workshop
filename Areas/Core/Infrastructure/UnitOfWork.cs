using System.Linq;
using DddWorkshop.Areas.AdminArea.Domain;
using DddWorkshop.Areas.Shop.Domain;
using Force.Ccc;
using Force.Ddd;
using Force.Ddd.DomainEvents;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public class UnitOfWork: IUnitOfWork
    {
        private readonly DbContext _dbContext;
        private readonly ICartStorage _cartStorage;

        public UnitOfWork(DbContext dbContext, ICartStorage cartStorage)
        {
            _dbContext = dbContext;
            _cartStorage = cartStorage;
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public void Add<TEntity>(TEntity entity) where TEntity : class, IHasId
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Remove<TEntity>(TEntity entity) where TEntity : class, IHasId
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public void Commit()
        {
            _cartStorage.SaveChanges();

            var domainEvents = _dbContext.ChangeTracker
                .Entries()
                .Where(x => x.Entity is IHasDomainEvents)
                .SelectMany(x => ((IHasDomainEvents) x).GetDomainEvents())
                .ToList();
            
            // TODO: dispatcher
            foreach (var de in domainEvents)
            {
                if (de is AuditLog al)
                {
                    _dbContext.Add(al);
                }
            }
            
            _dbContext.SaveChanges();
            
            
        }

        public void Rollback()
        {
        }
    }
}