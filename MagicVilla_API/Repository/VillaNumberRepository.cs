using MagicVilla_Api.Data;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository
{
    public class VillaNumberRepository : Repository<VillaNumber>, IVillaNumberRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaNumberRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<VillaNumber> Update(VillaNumber entity)
        {
            entity.UpdatedDate = DateTime.Now;
            if (_db.NumberVillas != null)
            {
                _db.NumberVillas.Update(entity);
                await _db.SaveChangesAsync();
                return entity;
            }
            else
            {
                throw new InvalidOperationException("No se puede actualizar la villa porque la colección _db.Villas es nula.");
            }
        }

    }
}
