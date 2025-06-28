using ETickets.SharedLibrary.Logs;
using ETickets.SharedLibrary.Responses;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.DTOs;
using ProductApi.Application.Intefaces;
using ProductApi.Domain.Entities;
using ProductApi.Infrastructure.Data;
using System.Linq.Expressions;

namespace ProductApi.Infrastructure.Repositories
{
    public class MovieRepository(MovieDbContext context) : IMovie
    {
        public async Task<Response> CreateAsync(Movie entity)
        {
            try
            {
                var getMovie = await GetByAsync(_ => _.Name.Equals(entity.Name));
                if (getMovie is not null)
                    return new Response(false, $"{entity.Name} already added");
                await context.Movies.AddAsync(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Name} added too database successfully");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error occurred during adding new movie");
            }
        }

        public async Task<Response> CreateAsync(CreateMovieDTO entity)
        {
            try
            {
                var getMovie = await GetByAsync(_ => _.Name.Equals(entity.Name));
                if (getMovie is not null)
                    return new Response(false, $"{entity.Name} already added");

                var movie = new Movie()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    Price = entity.Price,
                    ImageURL = entity.ImageURL,
                    CinemaId = entity.CinemaId,
                    ProducerId = entity.ProducerId,
                    MovieCategory = entity.MovieCategory,
                    StartDate = entity.StartDate,
                    EndDate = entity.EndDate
                };
                await context.AddAsync(movie);
                await context.SaveChangesAsync();

                foreach (var actorId in entity.ActorsIds)
                {
                    var actorMovie = new Actor_Movie() { MovieId = movie.Id, ActorId = actorId };
                    await context.Actor_Movies.AddAsync(actorMovie);
                }
                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Name} added too database successfully");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error occurred during Adding new movie");

            }
        }

        public async Task<Response> DeleteAsync(Movie entity)
        {
            try
            {
                var movie = await FindByIdAsync(entity.Id);
                if (movie is null)
                    return new Response(false, $"{entity.Name} not found");
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Name} is deleted succefully");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error occurred during deleting movie");
            }
        }

        public async Task<Response> DeleteByIdAsync(int id)
        {
            try
            {
                var movie = await FindByIdAsync(id);
                if (movie is null)
                    return new Response(false, $"not found");
                context.Movies.Remove(movie);
                await context.SaveChangesAsync();
                return new Response(true, "deleted succefully");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                return new Response(false, "Error occurred during deleting movie");
            }
        }

        public async Task<Movie> FindByIdAsync(int id, string? include = null)
        {
            try
            {
                var movie = await context.Movies
                    .Include(m => m.Cinema)
                    .Include(m => m.Producer)
                    .Include(m => m.Actors_Movies)
                    .ThenInclude(m => m.Actor)
                    .FirstOrDefaultAsync(m => m.Id == id);
                return movie is not null ? movie : null!;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred during find movie");
            }
        }

        public IQueryable<Movie> GetAll()
        {
            try
            {
                return context.Movies.AsNoTracking();
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new InvalidOperationException("Error occurred during getting movies");
            }
        }

        public async Task<IEnumerable<Movie>> GetAllAsync()
        {
            try
            {
                return await context.Movies
                    .Include(m => m.Cinema)
                    .Include(m => m.Producer)
                    .Include(m => m.Actors_Movies)
                    .ThenInclude(m => m.Actor)
                    .AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new InvalidOperationException("Error occurred during getting movies");
            }
        }

        public Task<IEnumerable<Movie>> GetAllAsync<TProperty>(Expression<Func<Movie, TProperty>> navigationPropertyPath)
        {
            throw new NotImplementedException();
        }

        public async Task<Movie> GetByAsync(Expression<Func<Movie, bool>> predicate)
        {
            try
            {
                return await context.Movies
                    .Include(m => m.Cinema)
                    .Include(m => m.Producer)
                    .Include(m => m.Actors_Movies)
                    .ThenInclude(m => m.Actor)
                    .Where(predicate)
                    .FirstOrDefaultAsync() ?? null!;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new Exception("Error occurred during getting movie");
            }
        }

        public async Task<Response> UpdateAsync(Movie entity)
        {
            try
            {
                var movie = await FindByIdAsync(entity.Id);
                if (movie is null) return new Response(false, $"{entity.Name} not found");
                context.Entry(movie).State = EntityState.Detached;
                context.Movies.Update(entity);
                await context.SaveChangesAsync();
                return new Response(true, $"{movie.Name} is updated successfully");

            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new InvalidOperationException("Error occurred during updateing movie");
            }
        }

        public async Task<Response> UpdateAsync(UpdateMovieDTO entity)
        {
            try
            {
                var dbMovie = await FindByIdAsync(entity.Id);
                if (dbMovie is null) return new Response(false, $"{entity.Name} not found");
                dbMovie.Name = entity.Name;
                dbMovie.Description = entity.Description;
                dbMovie.Price = entity.Price;
                dbMovie.ImageURL = entity.ImageURL;
                dbMovie.CinemaId = entity.CinemaId;
                dbMovie.ProducerId = entity.ProducerId;
                dbMovie.MovieCategory = entity.MovieCategory;
                dbMovie.StartDate = entity.StartDate;
                dbMovie.EndDate = entity.EndDate;
                await context.SaveChangesAsync();
                //Remove Exist Actors
                var existData = await context.Actor_Movies.Where(n => n.MovieId == entity.Id).ToListAsync();
                context.Actor_Movies.RemoveRange(existData);
                await context.SaveChangesAsync();

                foreach (var actorId in entity.ActorsIds)
                {
                    var actorMovie = new Actor_Movie() { MovieId = entity.Id, ActorId = actorId };
                    await context.Actor_Movies.AddAsync(actorMovie);
                }
                await context.SaveChangesAsync();
                return new Response(true, $"{entity.Name} added too database successfully");
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new InvalidOperationException("Error occurred during updateing movie");

            }
        }

        public async Task<Movie> GetMovieById(int id)
        {
            try
            {
                return await context.Movies
                .Include(m => m.Cinema)
                .Include(m => m.Producer)
                .Include(m => m.Actors_Movies).ThenInclude(m => m.Actor)
                .FirstOrDefaultAsync(m => m.Id == id) ?? null!;
            }
            catch (Exception ex)
            {
                LogException.LogExceptions(ex);
                throw new InvalidOperationException("Error occurred during getting movie");
            }
        }
    }
}