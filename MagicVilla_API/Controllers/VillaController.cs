using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Api.Models;
using MagicVilla_Api.Models.Dto;
using MagicVilla_Api.Data;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Identity.Client;
using Microsoft.EntityFrameworkCore.SqlServer.Query.Internal;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obteniendo todas las villas...");

            var villa = _db.Villas;

            if (villa != null)
            {
                return Ok(await villa.ToListAsync());

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

            if (_db.Villas == null)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);

            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<VillaCreateDto>> AddVilla([FromBody] VillaCreateDto villaDto)
        {

            if (_db.Villas == null || villaDto.Name == null)
            {
                return BadRequest();
            }

            if (villaDto == null)
            {
                return BadRequest(villaDto);
            }

            var existingVilla = await _db.Villas
                .FirstOrDefaultAsync(v => v.Name != null && v.Name.ToUpper() == villaDto.Name.ToUpper());

            if (existingVilla != null)
            {
                ModelState.AddModelError("NameExist", "Ya existe un registro con ese nombre");
                return BadRequest(ModelState);
            }


            Villa model = new()
            {
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                Fee = villaDto.Fee,
                SquareMeters = villaDto.SquareMeters,
                Amenity = villaDto.Amenity


            };
            await _db.Villas.AddAsync(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if (id == 0 || _db.Villas == null)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa == null)
            {
                return NotFound();
            }

            _db.Villas.Remove(villa);
            await _db.SaveChangesAsync();

            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto villaDto)
        {
            if (villaDto == null || id != villaDto.Id || _db.Villas == null)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                Fee = villaDto.Fee,
                SquareMeters = villaDto.SquareMeters,
                Amenity = villaDto.Amenity
            };

            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, villaDto);

        }

        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            if (patchDto == null || id == 0 || _db.Villas == null)
            {
                return BadRequest();
            }

            var villa = await _db.Villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return NotFound();
            }

            VillaUpdateDto villaDto = new()
            {
                Id = villa.Id,
                Name = villa.Name,
                Detail = villa.Detail,
                UrlImage = villa.UrlImage,
                Occupants = villa.Occupants,
                Fee = villa.Fee,
                SquareMeters = villa.SquareMeters,
                Amenity = villa.Amenity
            };

            patchDto.ApplyTo(villaDto, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            Villa model = new()
            {
                Id = villaDto.Id,
                Name = villaDto.Name,
                Detail = villaDto.Detail,
                UrlImage = villaDto.UrlImage,
                Occupants = villaDto.Occupants,
                Fee = villaDto.Fee,
                SquareMeters = villaDto.SquareMeters,
                Amenity = villaDto.Amenity
            };


            _db.Villas.Update(model);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("GetVilla", new { id = model.Id }, model);

        }

    };
}
