using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UsersIO.Api.DTOs;
using UsersIO.Business.Interfaces;
using UsersIO.Business.Models;

namespace UsersIO.Api.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : MainController
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UsersController(IUserRepository userRepository, IMapper mapper, IUserService userService, INotificator notificador) : base(notificador)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IEnumerable<UserDTO>> FindAllUsers()
        {
            return _mapper.Map<IEnumerable<UserDTO>>(await _userRepository.FindUsersComplete());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<UserDTO>> FindById(Guid id)
        {
            var user = _mapper.Map<UserDTO>(await _userRepository.FindUserComplete(id));

            if (user == null) return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<UserDTO>> AddUser([FromBody] UserDTO userDTO)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.Add(_mapper.Map<User>(userDTO));

            return Ok(userDTO);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult<UserDTO>> UpdateUser([FromRoute] Guid id, [FromBody] UserDTO userDTO)
        {
            if (id != userDTO.Id)
            {
                NotifyError("O id informado no objeto não é o mesmo enviado na query da requisição!");
                return CustomResponse(userDTO);
            }

            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _userService.Update(_mapper.Map<User>(userDTO));

            return CustomResponse(userDTO);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<UserDTO>> RemoveUser([FromRoute] Guid id)
        {
            var user = await _userRepository.FindUserComplete(id);

            if (user == null) return NotFound();

            await _userService.Remove(id);

            return CustomResponse(user);
        }

    }
}
