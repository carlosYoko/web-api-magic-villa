using MagicVilla_Api.Models.Dto;

namespace MagicVilla_Api.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id = 1, Name = "Vistas a la playa"},
            new VillaDto{Id = 2, Name = "Vistas a la piscina"}
        };
    }
}