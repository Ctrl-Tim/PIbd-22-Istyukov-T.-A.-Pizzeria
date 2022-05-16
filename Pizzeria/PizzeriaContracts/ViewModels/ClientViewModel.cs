using PizzeriaContracts.Attributes;
using System.Runtime.Serialization;

namespace PizzeriaContracts.ViewModels
{
    public class ClientViewModel
    {
        [Column(title: "Номер", width: 100)]
        [DataMember]
        public int Id { get; set; }

        [Column(title: "Клиент", width: 150)]
        [DataMember]
        public string ClientFIO { get; set; }

        [Column(title: "Email", width: 150)]
        [DataMember]
        public string Email { get; set; }

        [Column(title: "Пароль", width: 100)]
        [DataMember]
        public string Password { get; set; }
    }
}
