using System;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Models.Validations;

namespace DevIO.Business.Services
{
    public class ClienteService : BaseService, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly ITelefoneRepository _telefoneRepository;
        private readonly IMidiaSocialRepository _midiaSocialRepository;

        public ClienteService(IClienteRepository clienteRepository,
                              IEnderecoRepository enderecoRepository,
                              ITelefoneRepository telefoneRepository,
                              IMidiaSocialRepository midiaSocialRepository,
                              INotificador notificador) : base(notificador)
        {
            _clienteRepository = clienteRepository;
            _enderecoRepository = enderecoRepository;
            _telefoneRepository = telefoneRepository;
            _midiaSocialRepository = midiaSocialRepository;
        }

        public async Task<bool> Adicionar(Cliente cliente)
        {
            foreach (var endereco in cliente.Enderecos)
            {
                if (!ExecutarValidacao(new ClienteValidation(), cliente)
                    || !ExecutarValidacao(new EnderecoValidation(), endereco)) return false;
            }

            foreach (var telefone in cliente.Telefones)
            {
                if (!ExecutarValidacao(new ClienteValidation(), cliente)
                    || !ExecutarValidacao(new TelefoneValidation(), telefone)) return false;
            }

            if (_clienteRepository.Buscar(f => f.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um cliennte com este CPF informado.");
                return false;
            }

            if (_clienteRepository.Buscar(f => f.Rg == cliente.Rg).Result.Any())
            {
                Notificar("Já existe um cliente com este RG informado.");
                return false;
            }

            await _clienteRepository.Adicionar(cliente);
            return true;
        }

        public async Task<bool> Atualizar(Cliente cliente)
        {
            if (!ExecutarValidacao(new ClienteValidation(), cliente)) return false;

            if (_clienteRepository.Buscar(f => f.Cpf == cliente.Cpf).Result.Any())
            {
                Notificar("Já existe um cliennte com este CPF informado.");
                return false;
            }

            if (_clienteRepository.Buscar(f => f.Rg == cliente.Rg).Result.Any())
            {
                Notificar("Já existe um cliente com este RG informado.");
                return false;
            }

            await _clienteRepository.Atualizar(cliente);
            return true;
        }

        public async Task AtualizarEndereco(Endereco endereco)
        {
            if (!ExecutarValidacao(new EnderecoValidation(), endereco)) return;

            await _enderecoRepository.Atualizar(endereco);
        }

        public async Task<bool> Remover(Guid id)
        {

            var enderecos = await _enderecoRepository.ObterEnderecosPorCliente(id);

            if (enderecos != null && enderecos.Count() > 0)
            {
                foreach (var endereco in enderecos)
                {
                    await _enderecoRepository.Remover(endereco.Id);
                }
            }

            var telefones = await _telefoneRepository.ObterTelefonesPorCliente(id);

            if (telefones != null && telefones.Count() > 0)
            {
                foreach (var telefone in telefones)
                {
                    await _telefoneRepository.Remover(telefone.Id);
                }
            }

            var midiasSociais = await _midiaSocialRepository.ObterMidiasSociaisPorCliente(id);

            if (midiasSociais != null && midiasSociais.Count() > 0)
            {
                foreach (var midiaSocial in midiasSociais)
                {
                    await _midiaSocialRepository.Remover(midiaSocial.Id);
                }
            }

            await _clienteRepository.Remover(id);
            return true;
        }

        public void Dispose()
        {
            _clienteRepository?.Dispose();
            _enderecoRepository?.Dispose();
        }
    }
}