using System;

namespace UsersIO.Business.Models
{
    public class Address : Entity
    {
        public Guid UserId { get; set; }
        public string PublicPlace { get; set; }
        public string Number { get; set; }
        public string Complement { get; set; }
        public string Cep { get; set; }
        public string Neighborhood { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public AddressType AddressType { get; set; }
        public User User { get; set; }
    }
}
