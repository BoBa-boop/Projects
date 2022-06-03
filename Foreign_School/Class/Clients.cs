using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;
using System.Data.Linq.Mapping;
namespace Foreign_School.Class
{
    [Table(Name = "Client")]
    public class Clients
    {
        
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int Id { get; set; }
        [Column(Name = "FirstName")]
        public string Name { get; set; }
        [Column(Name = "LastName")]
        public string LastName { get; set; }
        [Column(Name = "Patronymic")]
        public string Otchestvo { get; set; }
        [Column(Name ="Birthday")]
        public DateTime Birthday { get; set; }
        [Column(Name = "RegistrationDate")]
        public DateTime Registrate { get; set; }
        [Column(Name = "Email")]
        public string Email { get; set; }
        [Column(Name = "Phone")]
        public string Phone { get; set; }
        [Column(Name = "GenderCode")]
        public string Sex { get; set; }
        [Column(Name = "PhotoPath")]
        public string Photo { get; set; }
        [Column(Name = "Status")]
        public bool status { get; set; }
    }
}
