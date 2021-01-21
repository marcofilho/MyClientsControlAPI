using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface ITelefoneRepository : IRepository<Telefone>
    {
        Task<IEnumerable<Telefone>> ObterTelefonesPorCliente(Guid fornecedorId);
        Task<IEnumerable<Telefone>> ObterTelefonesClientes();
    }
}