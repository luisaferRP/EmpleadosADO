using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTecnicaEmpleados.Api.Dtos
{
    public class UserResponseDto
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }
        public string Dni { get; set; }
        public string Email { get; set; }
    }
}