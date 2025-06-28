using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Application.DTOs.Conversions;
using ProductApi.Application.DTOs;
using ProductApi.Application.Intefaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace ProductApi.Presentaion.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ActorController(IActor actorInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetActors()
        {
            var actors = await actorInterface.GetAll().Include(a=> a.Actors_Movies).ThenInclude(ac=> ac.Movie).ToListAsync();
            if (!actors.Any()) return NotFound("No actors detected in database");
            return Ok(ActorConversion.FromEntity(null, actors).Item2);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetActor(int id)
        {
            var actor = await actorInterface.FindByIdAsync(id, "Actors_Movies.Movie");
            if (actor is null) return NotFound("not Found Actor");
            return Ok(ActorConversion.FromEntity(actor, null).Item1);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateActor(CreateActorDTO actorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await actorInterface.CreateAsync(ActorConversion.ToEntity(actorDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateActor(UpdateActorDTO actorDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await actorInterface.UpdateAsync(ActorConversion.ToEntity(actorDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteActor(ActorDTO actorDTO)
        {
            var response = await actorInterface.DeleteAsync(ActorConversion.ToEntity(actorDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
