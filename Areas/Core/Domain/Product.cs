using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.AdminArea.Domain;
using DddWorkshop.Areas.ProductManagement;
using DddWorkshop.Areas.Shop.Domain;
using Force.Ddd.DomainEvents;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Core.Domain
{
    public partial class Product: IntEntityBase, IHasDomainEvents
    {
        public static ProductSpecs Specs = new ProductSpecs();
        
        private DomainEventStore _domainEventStore = new DomainEventStore();
        
        // EF Only
        protected Product(){}
        
        public Product(string name, double price, int discountPercent)
        {
            // Validation?
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
        }

        [Required, StringLength(255)]
        public string Name { get; protected set; }
        
        [Range(0, 1000000)]
        public double Price { get; protected set; }
        
        [Range(0, 100)]
        public int DiscountPercent { get; protected set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;

        internal void Update(UpdateProduct command, [CanBeNull]IdentityUser user)
        {
            Name = command.Name;
            Price = command.Price;
            DiscountPercent = command.DiscountPercent;
            _domainEventStore.Raise(new AuditLog
            {
                EntityId = Id,
                EventName = "Product Updated",
                UserName = user?.UserName ?? "Anonymous"
            });
        }

        public IEnumerable<IDomainEvent> GetDomainEvents()
            => _domainEventStore;
    }

    public interface IHasDiscount
    {
        int Price { get; set; }
        
        int DiscountPercent { get; set; }

        decimal DiscountedPrice() => Price - Price / 100 * DiscountPercent;
    }
    
}