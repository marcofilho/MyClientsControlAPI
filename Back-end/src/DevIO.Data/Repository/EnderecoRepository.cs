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
    public class EnderecoRepository : Repository<Endereco>, IEnderecoRepository
    {
        public EnderecoRepository(MeuDbContext context) : base(context) { }

        public async Task<IEnumerable<Endereco>> ObterEnderecosClientes()
        {
            return await Db.Enderecos.AsNoTracking().Include(f => f.Cliente)
                .OrderBy(p => p.Cidade).ToListAsync();
        }

        public async Task<IEnumerable<Endereco>> ObterEnderecosPorCliente(Guid clienteId)
        {
            return await Buscar(p => p.ClienteId == clienteId);
        }
    }
}