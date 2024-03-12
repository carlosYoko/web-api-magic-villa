using MagicVilla_Api.Data;
using MagicVilla_Api.Models;

namespace MagicVilla_API.Repository
{
    public class VillaRepository : Repository<Villa>, IVillaRepository
    {
        private readonly ApplicationDbContext _db;

        public VillaRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public async Task<Villa> Update(Villa entity)
        {
            entity.DateUpdated = DateTime.Now;
            if (_db.Villas != null)
            {
                _db.Villas.Update(entity);
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
