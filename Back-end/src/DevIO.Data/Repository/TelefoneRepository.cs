using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Data.Repository
{
    public class TelefoneRepository : Repository<Telefone>, ITelefoneRepository
    {
        public TelefoneRepository(MeuDbContext context) : base(context) { }

        public async Task<Telefone> ObterTelefoneCliente(Guid id)
        {
            return await Db.Telefones.AsNoTracking().Include(f => f.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Telefone>> ObterTelefonesClientes()
        {
            return await Db.Telefones.AsNoTracking().Include(f => f.Cliente)
                .OrderBy(p => p.Numero).ToListAsync();
        }

        public async Task<IEnumerable<Telefone>> ObterTelefonesPorCliente(Guid clienteId)
        {
            return await Buscar(p => p.ClienteId == clienteId);
        }
    }
}