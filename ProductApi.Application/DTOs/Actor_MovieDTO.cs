using ProductApi.Domain.Data;

namespace ProductApi.Application.DTOs
{
    public record Actor_MovieDTO
    (
        int MovieId,
        MovieDTOResponse? Movie,
        int ActorId,
        ActorDTOResponse? Actor
    );
    public record MovieDTOResponse(int Id,
        decimal Price,
        DateTime StartDate,
        DateTime EndDate,
        MovieCategory MovieCategory,
        string Name = null!,
        string Description = null!,
        string ImageURL = null!);
    public record ActorDTOResponse
    (
        int Id,
        string ProfilePictureURL,
        string FullName,
        string Bio
    );

}
