using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs
{
    public record CinemaDTO
    (   int Id,
        [Required] string Logo,
        [Required] string Name,
        [Required] string Description
    );
}
