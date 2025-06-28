using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class ActorConversion
    {
        public static Actor ToEntity(CreateActorDTO actorDTO) => new()
        {
            ProfilePictureURL = actorDTO.ProfilePictureURL,
            FullName = actorDTO.FullName,
            Bio = actorDTO.Bio
        };
        public static Actor ToEntity(UpdateActorDTO actorDTO) => new()
        {
            Id = actorDTO.Id,
            ProfilePictureURL = actorDTO.ProfilePictureURL,
            FullName = actorDTO.FullName,
            Bio = actorDTO.Bio
        };
        public static Actor ToEntity(ActorDTO actorDTO) => new()
        {
            Id = actorDTO.Id,
            ProfilePictureURL = actorDTO.ProfilePictureURL,
            FullName = actorDTO.FullName,
            Bio = actorDTO.Bio
        };
        public static (ActorDTO?, IEnumerable<ActorDTO>?) FromEntity(Actor? actor, IEnumerable<Actor>? actors)
        {
            if (actor is not null)
            {
                var singleActor = new ActorDTO(
                    actor!.Id,
                    actor.ProfilePictureURL,
                    actor.FullName,
                    actor.Bio,
                    Actor_MovieConversion.FromEntity(null, actor.Actors_Movies).Item2
                    );
                return (singleActor, null);
            }
            if (actors is not null)
            {
                var _actors = actors!.Select(a =>
                new ActorDTO(
                    a!.Id,
                    a.ProfilePictureURL,
                    a.FullName,
                    a.Bio,
                    Actor_MovieConversion.FromEntity(null, a.Actors_Movies).Item2
                    )
                ).ToList();
                return (null, _actors);
            }
            return (null, null);
        }
    }
}
