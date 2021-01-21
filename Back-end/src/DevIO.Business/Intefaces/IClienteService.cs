using System;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface IClienteService : IDisposable
    {
        Task<bool> Adicionar(Cliente fornecedor);
        Task<bool> Atualizar(Cliente fornecedor);
        Task<bool> Remover(Guid id);

        Task AtualizarEndereco(Endereco endereco);
    }
}