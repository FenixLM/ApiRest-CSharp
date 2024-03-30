using BackendCurso.DTOs;
using BackendCurso.Models;
using BackendCurso.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace BackendCurso.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeerController : ControllerBase
    {
        private IValidator<BeerInsertDto> _beerInsertValidator;
        private ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> _beerService;

        public BeerController(
            IValidator<BeerInsertDto> beerInsertValidator,
            [FromKeyedServices("beerService")]ICommonService<BeerDto, BeerInsertDto, BeerUpdateDto> beerService
            ) 
        { 
            _beerInsertValidator = beerInsertValidator;
            _beerService = beerService;
        }

        [HttpGet]
        // IEnumerable es una interfaz que define un método que una colección debe implementar para poder recorrerla
        public async Task<IEnumerable<BeerDto>> Get() => 
           await  _beerService.Get();


        // ActionResult es una clase que representa un resultado de una acción 
        // Se encarga de devolver un resultado de una acción en codigo de estado HTTP
        [HttpGet("{id}")]
        public async Task<ActionResult<BeerDto>> GetById(int id)
        {
            var beerDto = await _beerService.GetById(id);
            if (beerDto == null) return NotFound();
            return Ok(beerDto);
        }

        [HttpPost]
        // Task se utiliza para devolver un resultado de una tarea asíncrona
        // ActionResult se implementa para devolver un resultado de una acción a comparacion de Ienumerable que se utiliza para recorrer una colección

        public async Task<ActionResult<BeerDto>> Add(BeerInsertDto beerInsertDto)
        {
            var validator = await _beerInsertValidator.ValidateAsync(beerInsertDto);
            if (!validator.IsValid) return BadRequest(validator.Errors);

            if (!_beerService.Validate(beerInsertDto)) return BadRequest(_beerService.Errors);

            var beerDto = await _beerService.Add(beerInsertDto);                     

            return CreatedAtAction(nameof(Get), new { id = beerDto.Id }, beerDto);

        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BeerDto>> Update(int id, BeerUpdateDto beerUpdateDto)
        {
            if (id != beerUpdateDto.Id) return BadRequest();


            var beerDto = await _beerService.Update(id, beerUpdateDto);


            return beerDto == null ? NotFound() : Ok(beerDto);
        }

        [HttpDelete("{id}")]

        public async Task<ActionResult<BeerDto>> Delete(int id)
        {
            var beer = await  _beerService.Delete(id);

            return beer == null ? NotFound() : Ok(beer);
        }
    }
}
