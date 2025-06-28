using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs.Conversions;
using ProductApi.Application.DTOs;
using ProductApi.Application.Intefaces;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class CinemaController(ICinema CinemaInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetCinemas()
        {
            var Cinemas = await CinemaInterface.GetAllAsync();
            if (!Cinemas.Any()) return NotFound("No Cinemas detected in database");
            return Ok(CinemaConversion.FromEntity(null, Cinemas).Item2);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCinema(int id)
        {
            var Cinema = await CinemaInterface.FindByIdAsync(id);
            if (Cinema is null) return NotFound("not Found Cinema");
            return Ok(CinemaConversion.FromEntity(Cinema, null).Item1);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCinema(CinemaDTO CinemaDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await CinemaInterface.CreateAsync(CinemaConversion.ToEntity(CinemaDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateCinema(CinemaDTO CinemaDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await CinemaInterface.UpdateAsync(CinemaConversion.ToEntity(CinemaDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteCinema(CinemaDTO CinemaDTO)
        {
            var response = await CinemaInterface.DeleteAsync(CinemaConversion.ToEntity(CinemaDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
