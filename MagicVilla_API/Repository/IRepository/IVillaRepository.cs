using MagicVilla_Api.Models;
using MagicVilla_API.IRepository;

namespace MagicVilla_API.Repository
{
    public interface IVillaRepository : IRepository<Villa>
    {
        Task<Villa> Update(Villa entity);
    }

}
