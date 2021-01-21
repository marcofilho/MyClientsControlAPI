using System;
using System.ComponentModel.DataAnnotations;

namespace UsersIO.Api.DTOs
{
    public class SocialMidiaDTO
    {

        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório.")]
        [EmailAddress(ErrorMessage = "O campo {0} está em formato inválido.")]
        public string Value { get; set; }

        public int SocialMidiaType { get; set; }

        public Guid UserId { get; set; }
    }

}

