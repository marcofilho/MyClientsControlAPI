using System;
using System.Collections.Generic;

namespace UsersIO.Business.Models
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string Cpf { get; set; }
        public string Rg { get; set; }
        public DateTime BirthdayDate { get; set; }
        public IList<SocialMidia> SocialMidias { get; set; }
        public IList<Phone> Phones { get; set; }
        public IList<Address> Adresses { get; set; }
    }
}
