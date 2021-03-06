﻿using System;
using System.ComponentModel.DataAnnotations;

namespace DevIO.Api.ViewModels
{
    public class TelefoneViewModel
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ClienteId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório")]
        [StringLength(14, ErrorMessage = "O campo {0} precisa ter entre {2} e {1} caracteres", MinimumLength = 8)]
        public string Numero { get; set; }
        public int TipoTelefone { get; set; }

        [ScaffoldColumn(false)]
        public string NomeCliente { get; set; }
    }
}