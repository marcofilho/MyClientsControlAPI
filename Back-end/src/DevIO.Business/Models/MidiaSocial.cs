using System;

namespace DevIO.Business.Models
{
    public class MidiaSocial : Entity
    {
        public Guid ClienteId { get; set; }
        public string Valor { get; set; }
        public TipoMidiaSocial TipoMidiaSocial { get; set; }

        /* EF Relations */
        public Cliente Cliente { get; set; }
    }
}