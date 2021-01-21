using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.Api.Controllers;
using DevIO.Api.Extensions;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DevIO.Api.V1.Controllers
{
    [Authorize]
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/clientes")]
    public class ClientesController : MainController
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly IClienteService _clienteService;
        private readonly IEnderecoRepository _enderecoRepository;
        private readonly IMapper _mapper;

        public ClientesController(IClienteRepository clienteRepository, 
                                      IMapper mapper, 
                                      IClienteService clienteService,
                                      INotificador notificador, 
                                      IEnderecoRepository enderecoRepository,
                                      IUser user) : base(notificador, user)
        {
            _clienteRepository = clienteRepository;
            _mapper = mapper;
            _clienteService = clienteService;
            _enderecoRepository = enderecoRepository;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IEnumerable<ClienteViewModel>> ObterTodos()
        {
            return _mapper.Map<IEnumerable<ClienteViewModel>>(await _clienteRepository.ObterTodos());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> ObterPorId(Guid id)
        {
            var cliente = await ObterClienteEnderecosTelefonesMidiaisSociais(id);

            if (cliente == null) return NotFound();

            return cliente;
        }

        [ClaimsAuthorize("cliente","Adicionar")]
        [HttpPost]
        public async Task<ActionResult<ClienteViewModel>> Adicionar(ClienteViewModel clienteViewModel)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Adicionar(_mapper.Map<Cliente>(clienteViewModel));

            return CustomResponse(clienteViewModel);
        }

        [ClaimsAuthorize("cliente", "Atualizar")]
        [HttpPut("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Atualizar(Guid id,[FromBody]ClienteViewModel clienteViewModel)
        {
            if (id != clienteViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(clienteViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.Atualizar(_mapper.Map<Cliente>(clienteViewModel));

            return CustomResponse(clienteViewModel);
        }

        [ClaimsAuthorize("cliente", "Excluir")]
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ClienteViewModel>> Excluir(Guid id)
        {
            var clienteViewModel = await ObterClienteEnderecosTelefonesMidiaisSociais(id);

            if (clienteViewModel == null) return NotFound();

            await _clienteService.Remover(id);

            return CustomResponse(clienteViewModel);
        }

        [HttpGet("endereco/{id:guid}")]
        public async Task<EnderecoViewModel> ObterEnderecoPorId(Guid id)
        {
            return _mapper.Map<EnderecoViewModel>(await _enderecoRepository.ObterPorId(id));
        }

        [ClaimsAuthorize("cliente", "Atualizar")]
        [HttpPut("endereco/{id:guid}")]
        public async Task<IActionResult> AtualizarEndereco(Guid id, EnderecoViewModel enderecoViewModel)
        {
            if (id != enderecoViewModel.Id)
            {
                NotificarErro("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(enderecoViewModel);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _clienteService.AtualizarEndereco(_mapper.Map<Endereco>(enderecoViewModel));

            return CustomResponse(enderecoViewModel);
        }

        private async Task<ClienteViewModel> ObterClienteEnderecosTelefonesMidiaisSociais(Guid id)
        {
            return _mapper.Map<ClienteViewModel>(await _clienteRepository.ObterClienteEnderecosTelefonesMidiasSociais(id));
        }

    }
}