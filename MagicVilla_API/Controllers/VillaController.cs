using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MagicVilla_Api.Models.Dto;
using MagicVilla_Api.Data;

namespace MagicVilla_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<VillaDto> GetVillas()
        {
            return VillaStore.villaList;

        }

        [HttpGet("id")]
        public VillaDto? GetVilla(int id)
        {
            return VillaStore.villaList.FirstOrDefault(v => v.Id == id);
        }
    }
}
