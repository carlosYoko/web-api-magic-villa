using MagicVilla_API.IRepository;
using MagicVilla_API.Models;

namespace MagicVilla_API.Repository
{
    public interface IVillaNumberRepository : IRepository<VillaNumber>
    {
        Task<VillaNumber> Update(VillaNumber entity);
    }

}
