using ProductApi.Domain.Data;
using System.ComponentModel.DataAnnotations;

namespace ProductApi.Application.DTOs
{
    public record MovieDTO(int Id, 
        [Required, DataType(DataType.Currency)] decimal Price,
        [Required] DateTime StartDate,
        [Required] DateTime EndDate,
        [Required] MovieCategory MovieCategory,
        [Required] int CinemaId,
        CinemaDTO? Cinema,
        [Required] int ProducerId,
        ProducerDTO? Producer,
        IEnumerable<Actor_MovieDTO>? Actor_MovieDTOs,
        [Required] string Name = null!,
        [Required] string Description = null!,
        [Required] string ImageURL = null!);
    public record CreateMovieDTO(
       [Required, DataType(DataType.Currency)] decimal Price,
       [Required] DateTime StartDate,
       [Required] DateTime EndDate,
       [Required] MovieCategory MovieCategory,
       [Required] int CinemaId,
       [Required] int ProducerId,
       [Required] List<int> ActorsIds,
       [Required] string Name = null!,
       [Required] string Description = null!,
       [Required] string ImageURL = null!);
    public record UpdateMovieDTO(
       [Required] int Id,
       [Required, DataType(DataType.Currency)] decimal Price,
       [Required] DateTime StartDate,
       [Required] DateTime EndDate,
       [Required] MovieCategory MovieCategory,
       [Required] int CinemaId,
       [Required] int ProducerId,
       [Required] List<int> ActorsIds,
       [Required] string Name = null!,
       [Required] string Description = null!,
       [Required] string ImageURL = null!);
}
