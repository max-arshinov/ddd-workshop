using System.ComponentModel.DataAnnotations;

namespace DddWorkshop.Models
{
    public class IntHasNameBase: IntHasIdBase
    {
        [Required]
        public string Name { get; set; }

        public override string ToString() => Name;
    }
}