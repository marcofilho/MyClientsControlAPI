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
    public class MidiaSocialRepository : Repository<MidiaSocial>, IMidiaSocialRepository
    {
        public MidiaSocialRepository(MeuDbContext context) : base(context) { }

        public async Task<MidiaSocial> ObterMidiaSocialCliente(Guid id)
        {
            return await Db.MidiasSociais.AsNoTracking().Include(f => f.Cliente)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<MidiaSocial>> ObterMidiasSociaisClientes()
        {
            return await Db.MidiasSociais.AsNoTracking().Include(f => f.Cliente)
                .OrderBy(p => p.Valor).ToListAsync();
        }

        public async Task<IEnumerable<MidiaSocial>> ObterMidiasSociaisPorCliente(Guid clienteId)
        {
            return await Buscar(p => p.ClienteId == clienteId);
        }
    }
}