using System.ComponentModel.DataAnnotations;
using ETickets.SharedLibrary.Data;
namespace ProductApi.Domain.Entities
{
    public class Actor : IBaseEntity
    {
        public int Id { get; set; }
        [Display(Name = "Profile Picture")]
        [Required(ErrorMessage = "Profile Picture Is Required")]
        public string ProfilePictureURL { get; set; } = null!;
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Full Name Must Be Between 3 and 50 chars")]
        [Display(Name = "Full Name")]
        [Required(ErrorMessage = "Full Name Is Required")]
        public string FullName { get; set; } = null!;
        [Display(Name = "Biography")]
        [Required(ErrorMessage = "Biography Is Required")]
        public string Bio { get; set; } = null!;
        //Relationships
        public List<Actor_Movie> Actors_Movies { get; set; }
    }
}
