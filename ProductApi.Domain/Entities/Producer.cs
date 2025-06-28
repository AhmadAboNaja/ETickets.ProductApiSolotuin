using System.ComponentModel.DataAnnotations;
using ETickets.SharedLibrary.Data;
namespace ProductApi.Domain.Entities
{
    public class Producer : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture Is Required")]
        public string ProfilePictureURL { get; set; } = null!;
        [Display(Name = "Full Name")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name Must Be Between 3 and 50 chars")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; } = null!;
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography Is Required")]
        public string Bio { get; set; } = null!;
        //Relationships
        public List<Movie> Movies { get; set; }
    }
}
