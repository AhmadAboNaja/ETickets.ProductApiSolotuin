using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProductApi.Application.DTOs;
using ProductApi.Application.DTOs.Conversions;
using ProductApi.Application.Intefaces;

namespace ProductApi.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class MovieController(IMovie movieInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetMovies()
        {
            var movies = await movieInterface.GetAll().Include(m=> m.Cinema).ToListAsync();
            if (!movies.Any()) return NotFound("No Movies detected in database");
            return Ok(MovieConverisons.FromEntity(null, movies).Item2);
            //return Ok(movies);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetMovie(int id)
        {
            var movie = await movieInterface.GetMovieById(id);
            if (movie is null) return NotFound("not Found Movie");
            return Ok(MovieConverisons.FromEntity(movie,null).Item1);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateMovie(CreateMovieDTO movieDTO)
        {
            if(!ModelState.IsValid) return BadRequest(ModelState);
            var response = await movieInterface.CreateAsync(movieDTO);
            return response.Flag ? Ok(response):BadRequest(response);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateMove(UpdateMovieDTO movieDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await movieInterface.UpdateAsync(movieDTO);
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteMovie(MovieDTO movieDTO)
        {
            var response = await movieInterface.DeleteAsync(MovieConverisons.ToEntity(movieDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
