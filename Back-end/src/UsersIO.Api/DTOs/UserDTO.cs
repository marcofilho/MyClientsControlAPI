using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UsersIO.Api.DTOs
{
    public class UserDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 3)]
        public string Name { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(11, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 11)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(7, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 7)]
        public string Rg { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        public DateTime? BirthdayDate { get; set; }

        public IList<PhoneDTO> Phones { get; set; }

        public IList<AddressDTO> Adresses { get; set; }

        public IList<SocialMidiaDTO> SocialMidias { get; set; }


    }
}
