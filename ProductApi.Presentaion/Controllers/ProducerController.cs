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
    public class ProducerController(IProducer ProducerInterface) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetProducers()
        {
            var Producers = await ProducerInterface.GetAll().Include(p=> p.Movies).ToListAsync();
            if (!Producers.Any()) return NotFound("No Producers detected in database");
            return Ok(ProducerConversion.FromEntity(null, Producers).Item2);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetProducer(int id)
        {
            var Producer = await ProducerInterface.FindByIdAsync(id);
            if (Producer is null) return NotFound("not Found Producer");
            return Ok(ProducerConversion.FromEntity(Producer, null).Item1);
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateProducer(CreateProducerDTO ProducerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await ProducerInterface.CreateAsync(ProducerConversion.ToEntity(ProducerDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> UpdateProducer(UpdateProducerDTO ProducerDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var response = await ProducerInterface.UpdateAsync(ProducerConversion.ToEntity(ProducerDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteProducer(ProducerDTO ProducerDTO)
        {
            var response = await ProducerInterface.DeleteAsync(ProducerConversion.ToEntity(ProducerDTO));
            return response.Flag ? Ok(response) : BadRequest(response);
        }
    }
}
