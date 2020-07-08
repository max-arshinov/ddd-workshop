using System.ComponentModel.DataAnnotations;

namespace DotNext.DddWorkshop.Models
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