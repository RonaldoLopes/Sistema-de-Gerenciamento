
using SG.Domain.Entities;
using System.Threading.Tasks;

namespace SG.Repository.Repositories
{
    public interface ISGRepository
    {
        void Add<T>(T entity) where T : class;
        void Update<T>(T entity) where T : class;
        void Delete<T>(T entity) where T : class;
        Task<bool> SaveChangesAsync();

        Task<Cliente[]> GetAllClientesAsync();
        Task<Cliente> GetAllClientesById(int CliId);
    }
}
