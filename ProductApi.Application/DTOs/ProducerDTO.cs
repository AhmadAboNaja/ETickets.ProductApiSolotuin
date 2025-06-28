using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs
{
    public record ProducerDTO
    (
        int Id,
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio,
        IEnumerable<MovieDTOResponse>? MovieDTOs = null
    );
    public record CreateProducerDTO
    (
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio
    );
    public record UpdateProducerDTO
    (
        [Required] int Id,
        [Required] string ProfilePictureURL,
        [Required] string FullName,
        [Required] string Bio
    );
}
