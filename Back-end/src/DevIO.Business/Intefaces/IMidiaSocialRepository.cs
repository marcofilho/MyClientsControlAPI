using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface IMidiaSocialRepository : IRepository<MidiaSocial>
    {
        Task<IEnumerable<MidiaSocial>> ObterMidiasSociaisPorCliente(Guid clienteId);
        Task<IEnumerable<MidiaSocial>> ObterMidiasSociaisClientes();
    }
}