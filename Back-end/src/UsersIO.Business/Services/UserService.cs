using UsersIO.Business.Models.Validations;
using UsersIO.Business.Interfaces;
using UsersIO.Business.Models;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace UsersIO.Business.Services
{
    public class UserService : BaseService, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IPhoneRepository _phoneRepository;
        private readonly ISocialMidiaRepository _socialMidiaRepository;

        public UserService(IUserRepository userRepository,
                                 IAddressRepository addressRepository,
                                 IPhoneRepository phoneRepository,
                                 ISocialMidiaRepository socialMidiaRepository,
                                 INotificator notificador) : base(notificador)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _phoneRepository = phoneRepository;
            _socialMidiaRepository = socialMidiaRepository;
        }

        public async Task<bool> Add(User user)
        {
            foreach (var address in user.Adresses)
            {
                if (!ExecuteNotify(new UserValidation(), user)
                    || !ExecuteNotify(new AddressValidation(), address)) return false;

            }

            foreach (var phone in user.Phones)
            {
                if (!ExecuteNotify(new UserValidation(), user)
                    || !ExecuteNotify(new PhoneValidation(), phone)) return false;

            }

            foreach (var socialMidia in user.SocialMidias)
            {
                if (!ExecuteNotify(new UserValidation(), user)
                    || !ExecuteNotify(new SocialMidiaValidation(), socialMidia)) return false;

            }

            if (_userRepository.FindAsync(f => f.Cpf == user.Cpf).Result.Any())
            {
                Notify("Já existe um usuário com este CPF informado.");
                return false;
            }

            await _userRepository.Add(user);
            return true;
        }

        public async Task<bool> Update(User user)
        {
            if (!ExecuteNotify(new UserValidation(), user)) return false;

            if (_userRepository.FindAsync(f => f.Cpf == user.Cpf && f.Id != user.Id).Result.Any())
            {
                Notify("Já existe um usuário com este CPF informado.");
                return false;
            }

            await _userRepository.Update(user);
            return true;
        }

        public async Task UpdateAddress(Address endereco)
        {
            if (!ExecuteNotify(new AddressValidation(), endereco)) return;

            await _addressRepository.Update(endereco);
        }

        public async Task UpdatePhone(Phone phone)
        {
            if (!ExecuteNotify(new PhoneValidation(), phone)) return;

            await _phoneRepository.Update(phone);
        }

        public async Task UpdateSocialMidia(SocialMidia socialMidia)
        {
            if (!ExecuteNotify(new SocialMidiaValidation(), socialMidia)) return;

            await _socialMidiaRepository.Update(socialMidia);
        }

        public async Task<bool> Remove(Guid id)
        {

            var adresses = await _addressRepository.FindAdressesByUser(id);

            if (adresses != null && adresses.Count > 0)
            {
                foreach (var address in adresses)
                {
                    await _addressRepository.Remove(address.Id);
                }

            }

            var phones = await _phoneRepository.FindPhonesByUser(id);

            if (phones != null && phones.Count > 0)
            {
                foreach (var phone in phones)
                {
                    await _phoneRepository.Remove(phone.Id);
                }
            }

            var socialMidias = await _socialMidiaRepository.FindSocialMidiasByUser(id);

            if (socialMidias != null && socialMidias.Count > 0)
            {
                foreach (var socialMidia in socialMidias)
                {
                    await _socialMidiaRepository.Remove(socialMidia.Id);
                }
            }

            await _userRepository.Remove(id);
            return true;
        }

        public void Dispose()
        {
            _userRepository?.Dispose();
            _addressRepository?.Dispose();
            _phoneRepository?.Dispose();
            _socialMidiaRepository?.Dispose();
        }

    }
}