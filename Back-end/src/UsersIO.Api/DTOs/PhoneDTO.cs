using System;
using System.ComponentModel.DataAnnotations;

namespace UsersIO.Api.DTOs
{
    public class PhoneDTO
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(200, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 9)]
        public string Number { get; set; }

        public int PhoneType { get; set; }

        public Guid UserId { get; set; }

    }
}
