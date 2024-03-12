using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.Dto;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using MagicVilla_API.Repository;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IMapper _mapper;

        public VillaController(ILogger<VillaController> logger, IVillaRepository villaRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo todas las villas...");


            if (_villaRepo != null)
            {
                IEnumerable<Villa> villaList = await _villaRepo.GetAll();
                return Ok(_mapper.Map<IEnumerable<VillaDto>>(villaList));
            }
            else
            {
                return NotFound("No se encontraron villas...");
            }

        }

        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<VillaDto?>> GetVilla(int id)
        {
            if (!ModelState.IsValid || id == 0)
            {
                _logger.LogError($"Error al obtener la villa con el id: {id}");
                return BadRequest();
            }

            if (_villaRepo.Get == null)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Get(v => v.Id == id);

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDto>> AddVilla([FromBody] VillaCreateDto createDto)
        {

            if (_villaRepo == null || createDto.Name == null)
            {
                return BadRequest();
            }

            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            var existingVilla = await _villaRepo.Get(v => v.Name != null && v.Name.ToUpper() == createDto.Name.ToUpper());

            if (existingVilla != null)
            {
                ModelState.AddModelError("NameExist", "Ya existe un registro con ese nombre");
                return BadRequest(ModelState);
            }

            Villa model = _mapper.Map<Villa>(createDto);

            await _villaRepo.Create(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0 || _villaRepo == null)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Get(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            await _villaRepo.Remove(villa);

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            if (updateDto == null || id != updateDto.Id || _villaRepo == null)
            {
                return BadRequest();
            }
            Villa model = _mapper.Map<Villa>(updateDto);

            await _villaRepo.Update(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, updateDto);

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0 || _villaRepo == null)
            {
                return BadRequest();
            }

            var villa = await _villaRepo.Get(v => v.Id == id, tracked: false);

            if (villa == null)
            {
                return NotFound();
            }

            VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa);

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa model = _mapper.Map<Villa>(villaDto);

            await _villaRepo.Update(model);

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);

        }

    };
}
