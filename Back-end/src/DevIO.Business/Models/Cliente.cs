using System;
using System.Collections.Generic;

namespace DevIO.Business.Models
{
    public class Cliente : Entity
    {
        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime DataNascimento { get; set; }
        public IEnumerable<Telefone> Telefones { get; set; }
        public IEnumerable<Endereco> Enderecos { get; set; }
        public IEnumerable<MidiaSocial> MidiasSociais { get; set; }

    }
}