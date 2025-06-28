using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class MovieConverisons
    {
        public static Movie ToEntity(MovieDTO movieDTO) => new()
        {
            Id = movieDTO.Id,
            Price = movieDTO.Price,
            Description = movieDTO.Description,
            EndDate = movieDTO.EndDate,
            ImageURL = movieDTO.ImageURL,
            CinemaId = movieDTO.CinemaId,
            ProducerId = movieDTO.ProducerId,
            MovieCategory = movieDTO.MovieCategory,
            Name = movieDTO.Name,
            StartDate = movieDTO.StartDate 
        };

        public static (MovieDTO?, IEnumerable<MovieDTO>?) FromEntity(Movie? movie, IEnumerable<Movie>? movies)
        {
            if(movie is not null)
            {
                var singleMovie = new MovieDTO(
                    movie!.Id,
                    movie.Price,
                    movie.StartDate,
                    movie.EndDate,
                    movie.MovieCategory,
                    movie.CinemaId,
                    CinemaConversion.FromEntity(movie.Cinema, null).Item1?? null,
                    movie.ProducerId,
                    ProducerConversion.FromEntity(movie.Producer ?? null, null).Item1??null,
                    Actor_MovieConversion.FromEntity(null, movie.Actors_Movies ?? null).Item2??null,
                    movie.Name,
                    movie.Description,
                    movie.ImageURL
                    );
                return (singleMovie, null);
            }
            if (movies is not null)
            {
                var _movies = movies!.Select(m =>
                new MovieDTO(
                    m!.Id,
                    m.Price,
                    m.StartDate,
                    m.EndDate,
                    m.MovieCategory,
                    m.CinemaId,
                    CinemaConversion.FromEntity(m.Cinema, null).Item1,
                    m.ProducerId,
                    ProducerConversion.FromEntity(m.Producer, null).Item1,
                    Actor_MovieConversion.FromEntity(null, m.Actors_Movies).Item2,
                    m.Name,
                    m.Description,
                    m.ImageURL
                    )
                ).ToList();
                return (null, _movies);
            }
            return (null, null);
        }
    }
}
