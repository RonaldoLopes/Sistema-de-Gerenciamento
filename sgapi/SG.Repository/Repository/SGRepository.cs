using Microsoft.EntityFrameworkCore;
using SG.Domain.Entities;
using SG.Repository.Context;
using SG.Repository.Repositories;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SG.Repository.Repository
{
    public class SGRepository : ISGRepository
    {
        private readonly SGContext _context;
        #region metodos gerais
        public void Add<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public void Delete<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }

        public Task<Cliente[]> GetAllClientesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> SaveChangesAsync()
        {
            throw new NotImplementedException();
        }

        public void Update<T>(T entity) where T : class
        {
            throw new NotImplementedException();
        }
        #endregion
        #region metodos clientes
        public async Task<Cliente[]> GetClientesAsync()
        {
            IQueryable<Cliente> query = _context.Clientes
                .AsNoTracking();
            return await query.ToArrayAsync();
        }
        public async Task<Cliente> GetAllClientesById(int CliId)
        {
            IQueryable<Cliente> query = _context.Clientes
                .AsNoTracking()
                .Where(c => c.Id == CliId);
            return await query.FirstOrDefaultAsync();
        }
        #endregion
    }
}
