using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class Actor_MovieConversion
    {
        public static Actor_Movie ToEntity(Actor_MovieDTO actor_MovieDTO) => new()
        {
            ActorId = actor_MovieDTO.ActorId,
            MovieId = actor_MovieDTO.MovieId,
        };
        public static (Actor_MovieDTO?, IEnumerable<Actor_MovieDTO>?) FromEntity(Actor_Movie? actor_Movie, IEnumerable<Actor_Movie>? actor_Movies)
        {
            if (actor_Movie is not null)
            {
                var singleactor_Movie = new Actor_MovieDTO(
                    actor_Movie!.MovieId,
                    MovieResponseConverisons.FromEntity(actor_Movie.Movie, null).Item1,
                    actor_Movie.ActorId,
                    new ActorDTOResponse(actor_Movie.Actor.Id, actor_Movie.Actor.ProfilePictureURL, actor_Movie.Actor.FullName, actor_Movie.Actor.Bio)
                    );
                return (singleactor_Movie, null);
            }
            if (actor_Movies is not null)
            {
                var _actor_Movies = actor_Movies!.Select(a =>
                new Actor_MovieDTO(
                    a!.MovieId,
                    MovieResponseConverisons.FromEntity(a.Movie,null).Item1,
                    a.ActorId,
                    new ActorDTOResponse(a.Actor.Id, a.Actor.ProfilePictureURL, a.Actor.FullName, a.Actor.Bio)
                    )
                ).ToList();
                return (null, _actor_Movies);
            }
            return (null, null);
        }
    }
}
