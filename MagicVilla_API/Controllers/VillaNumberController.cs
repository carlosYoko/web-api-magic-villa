using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_API.Repository;
using MagicVilla_API.Models;
using System.Net;
using MagicVilla_API.Models.Dto;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly ILogger<VillaNumberController> _logger;
        private readonly IVillaRepository _villaRepo;
        private readonly IVillaNumberRepository _numberRepo;
        private readonly IMapper _mapper;

        protected APIResponse _response;

        public VillaNumberController(ILogger<VillaNumberController> logger, IVillaRepository villaRepo,
                                                                            IVillaNumberRepository numberRepo, IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _mapper = mapper;
            _response = new();
            _numberRepo = numberRepo;

        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetNumbersVillas()
        {

            try
            {
                _logger.LogInformation("OBtener el numero de villas...");

                IEnumerable<VillaNumber> numberVillaList = await _numberRepo.GetAll();

                _response.Result = _mapper.Map<IEnumerable<VillaNumberDto>>(numberVillaList);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };

            }

            return _response;

        }

        [HttpGet("id:int", Name = "GetNumberVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetNumberVilla(int id)
        {
            try
            {
                if (!ModelState.IsValid || id == 0)
                {
                    _logger.LogError($"Error al obtener la villa con el id: {id}");
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numberVilla = await _numberRepo.Get(v => v.VillaNum == id);
                if (numberVilla == null)
                {
                    _response.statusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }

                _response.Result = _mapper.Map<VillaNumberDto>(numberVilla);
                _response.statusCode = HttpStatusCode.OK;

                return Ok(_response);

            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<APIResponse>> AddNumberVilla([FromBody] VillaNumberCreateDto createDto)
        {
            try
            {

                if (_numberRepo == null)
                {
                    return BadRequest();
                }

                if (await _numberRepo.Get(v => v.VillaNum == createDto.VillaNum) != null)
                {
                    ModelState.AddModelError("NameExist", "Ya existe un registro con ese nombre");
                    return BadRequest(ModelState);
                }

                if (await _villaRepo.Get(v => v.Id == createDto.VillaId) == null)
                {
                    ModelState.AddModelError("ForeingKey", "Ya existe esta ID");
                    return BadRequest(ModelState);
                }

                if (createDto == null)
                {
                    return BadRequest(createDto);
                }

                VillaNumber model = _mapper.Map<VillaNumber>(createDto);
                model.CreationDate = DateTime.Now;
                model.UpdatedDate = DateTime.Now;

                await _numberRepo.Create(model);
                _response.Result = model;
                _response.statusCode = HttpStatusCode.Created;

                return CreatedAtRoute("GetNumberVilla", new { id = model.VillaNum }, _response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string> { ex.ToString() };
            }

            return _response;

        }

        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> DeleteNumberVilla(int id)
        {
            try
            {

                if (id == 0)
                {
                    _response.IsSuccess = false;
                    _response.statusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }

                var numberVilla = await _numberRepo.Get(v => v.VillaNum == id);
                if (numberVilla == null)
                {
                    _response.IsSuccess = false;
                    _response.statusCode = HttpStatusCode.NotFound;
                    return BadRequest(_response);
                }

                await _numberRepo.Remove(numberVilla);

                _response.statusCode = HttpStatusCode.NoContent;

                return Ok(_response);
            }
            catch (Exception ex)
            {

                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }

            return _response;
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateNumberVilla(int id, [FromBody] VillaNumberDto updateDto)
        {
            if (updateDto == null || id != updateDto.VillaNum || _villaRepo == null)
            {
                _response.IsSuccess = false;
                _response.statusCode = HttpStatusCode.BadRequest;
                return BadRequest(_response);
            }

            if (await _villaRepo.Get(v => v.Id == updateDto.VillaId) == null)
            {
                ModelState.AddModelError("ForeignKey", "El ID de la villa no existe...");
                return BadRequest(ModelState);
            }

            VillaNumber model = _mapper.Map<VillaNumber>(updateDto);

            await _numberRepo.Update(model);
            _response.statusCode = HttpStatusCode.NoContent;

            return Ok(_response);

        }


    };
}
