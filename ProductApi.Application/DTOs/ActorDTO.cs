using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs
{
    public record ActorDTO
    (
        int Id,
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio,
        IEnumerable<Actor_MovieDTO>? Actor_MovieDTOs = null
    );
    public record CreateActorDTO
    (
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio
    );
    public record UpdateActorDTO
    (
        [Required] int Id,
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio
    );
}
