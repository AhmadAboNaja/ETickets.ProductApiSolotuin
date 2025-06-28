using ProductApi.Domain.Entities;

namespace ProductApi.Application.DTOs.Conversions
{
    public static class MovieResponseConverisons
    {
        public static (MovieDTOResponse?, IEnumerable<MovieDTOResponse>?) FromEntity(Movie? movie, IEnumerable<Movie>? movies)
        {
            if(movie is not null)
            {
                var singleMovie = 
                    new MovieDTOResponse(
                        movie.Id, 
                        movie.Price, 
                        movie.StartDate, 
                        movie.EndDate, 
                        movie.MovieCategory, 
                        movie.Name, 
                        movie.Description,
                        movie.ImageURL);
                return (singleMovie, null);
            }
            if (movies is not null)
            {
                var _movies = movies!.Select(m =>
                new MovieDTOResponse(
                        m.Id,
                        m.Price,
                        m.StartDate,
                        m.EndDate,
                        m.MovieCategory,
                        m.Name,
                        m.Description,
                        m.ImageURL)
                ).ToList();
                return (null, _movies);
            }
            return (null, null);
        }
    }
}
