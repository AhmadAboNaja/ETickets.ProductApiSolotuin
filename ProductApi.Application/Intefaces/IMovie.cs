using ETickets.SharedLibrary.Interface;
using ETickets.SharedLibrary.Responses;
using ProductApi.Application.DTOs;
using ProductApi.Domain.Entities;

namespace ProductApi.Application.Intefaces
{
    public interface IMovie : IGenericInterface<Movie> 
    {
        Task<Response> CreateAsync(CreateMovieDTO entity);
        Task<Response> UpdateAsync(UpdateMovieDTO entity);
        Task<Movie> GetMovieById(int id);
    }
}
