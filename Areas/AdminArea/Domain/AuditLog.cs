using System;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;
using Force.Ddd.DomainEvents;

namespace DddWorkshop.Areas.AdminArea.Domain
{
    public class AuditLog: IntEntityBase, IDomainEvent
    {
        [Required]
        public string EventName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public int? EntityId { get; set; }

        public override string ToString() =>
            $"{UserName} / {EventName} / {EntityId}";

        public DateTime Happened { get; } = DateTime.UtcNow;
    }
}