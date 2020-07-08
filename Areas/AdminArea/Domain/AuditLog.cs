using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;

namespace DddWorkshop.Areas.AdminArea.Domain
{
    public class AuditLog: IntEntityBase
    {
        [Required]
        public string EventName { get; set; }
        
        [Required]
        public string UserName { get; set; }
        
        public int? EntityId { get; set; }

        public override string ToString() =>
            $"{UserName} / {EventName} / {EntityId}";
    }
}