using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class CinemaConversion
    {
        public static Cinema ToEntity(CinemaDTO cinemaDTO) => new()
        {
            Id = cinemaDTO.Id,
            Description = cinemaDTO.Description,
            Name = cinemaDTO.Name,
            Logo = cinemaDTO.Logo
        };
        public static (CinemaDTO?, IEnumerable<CinemaDTO>?) FromEntity(Cinema? cinema, IEnumerable<Cinema>? cinemas)
        {
            if (cinema != null)
            {
                var singleCinema = new CinemaDTO(
                    cinema!.Id,
                    cinema.Name,
                    cinema.Description,
                    cinema.Logo
                    );
                return (singleCinema, null);
            }
            if (cinemas != null)
            {
                var _cinemas = cinemas!.Select(c =>
                new CinemaDTO(
                    c!.Id,
                    c.Logo,
                    c.Name,
                    c.Description
                    )
                ).ToList();
                return (null, _cinemas);
            }
            return (null, null);
        }
    }
}
