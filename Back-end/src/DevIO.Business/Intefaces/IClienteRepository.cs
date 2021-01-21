using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClienteEndereco(Guid id);
        Task<Cliente> ObterClienteTelefones(Guid id);
        Task<Cliente> ObterClienteMidiasSociais(Guid id);
        Task<Cliente> ObterClienteEnderecosTelefonesMidiasSociais(Guid id);

    }
}