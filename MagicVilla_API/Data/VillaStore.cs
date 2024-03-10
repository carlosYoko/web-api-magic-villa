using MagicVilla_Api.Models.Dto;

namespace MagicVilla_Api.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id = 1, Name = "Vistas a la playa", Occupants = 4, SquareMeters = 45},
            new VillaDto{Id = 2, Name = "Vistas a la piscina", Occupants = 6, SquareMeters = 65}
        };
    }
}