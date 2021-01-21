using System;

namespace DevIO.Business.Models
{
    public class Telefone : Entity
    {
        public Guid ClienteId { get; set; }
        public string Numero { get; set; }
        public TipoTelefone TipoTelefone { get; set; }

        /* EF Relations */
        public Cliente Cliente { get; set; }
    }
}