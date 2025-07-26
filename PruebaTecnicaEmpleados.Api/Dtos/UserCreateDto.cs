using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PruebaTecnicaEmpleados.Api.Dtos
{
    public class UserCreateDto
    {
        [Required]
        [StringLength(50, MinimumLength = 3)]
        public string Name { get; set; }

        public string LastName { get; set; }
        public string Address { get; set; }

        [Phone]
        public string Phone { get; set; }
        public DateTime Birthdate { get; set; }

        [Required]
        [StringLength(20, MinimumLength = 8)]
        public string Dni { get; set; }


        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}