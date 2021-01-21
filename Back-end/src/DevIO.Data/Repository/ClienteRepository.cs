using System;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(MeuDbContext context) : base(context)
        {
        }

        public async Task<Cliente> ObterClienteEndereco(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
                .Include(c => c.Enderecos)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteTelefones(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
                .Include(c => c.Telefones)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteMidiasSociais(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
                .Include(c => c.MidiasSociais)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteEnderecosTelefonesMidiasSociais(Guid id)
        {
            return await Db.Clientes.AsNoTracking()
                .Include(c => c.Enderecos)
                .Include(c => c.Telefones)
                .Include(c => c.MidiasSociais)
                .FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}