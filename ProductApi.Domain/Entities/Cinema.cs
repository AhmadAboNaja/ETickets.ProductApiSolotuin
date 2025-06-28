using System.ComponentModel.DataAnnotations;
using ETickets.SharedLibrary.Data;

namespace ProductApi.Domain.Entities
{
    public class Cinema : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Logo")]
        [Required(ErrorMessage = "Cinema Logo Is Required")]
        public string Logo { get; set; } = null!;
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Name Between 3 And 50")]
        [Display(Name = "Name")]
        [Required(ErrorMessage = "Cinema Name Is Required")]
        public string Name { get; set; } = null!;
        [Display(Name = "Description")]
        [Required(ErrorMessage = "Cinema Description Is Required")]
        public string Description { get; set; } = null!;
    }
}
