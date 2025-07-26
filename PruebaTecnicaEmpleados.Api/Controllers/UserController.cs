using System;
using System.Net;
using System.Web.Http;
using PruebaTecnicaEmpleados.Api.Services.Interface;
using System.Threading.Tasks;
using PruebaTecnicaEmpleados.Api.Models;
using PruebaTecnicaEmpleados.Api.Dtos;



namespace PruebaTecnicaEmpleados.Api.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var response = await _userService.GetAllUsersAsync();
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return InternalServerError(new Exception(response.Message));
        }

        /// <summary>
        /// Obtener usuario por el id
        /// </summary>
        [HttpGet]
        [Route("{id:int}", Name = "GetUserById")]
        public async Task<IHttpActionResult> Get(int id)
        {
            var response = await _userService.GetUserByIdAsync(id);
            if (response.Success)
            {
                return Ok(response.Data);
            }
            return NotFound();
        }

        /// <summary>
        /// Crear un nuevo usuario
        /// </summary>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Post([FromBody] UserCreateDto dtoUser)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var user = new User
            {
               
                Name = dtoUser.Name,
                LastName = dtoUser.LastName,
                Address = dtoUser.Address,
                Phone = dtoUser.Phone,
                Birthdate = dtoUser.Birthdate,
                Dni = dtoUser.Dni,
                Email = dtoUser.Email
            };
            var response = await _userService.AddUserAsync(user);
            if (!response.Success)
            {
                return InternalServerError(new Exception(response.Message));
            }
            var createdUser = response.Data;
            var userReadDto = new UserResponseDto
            {
                Name = createdUser.Name,
                LastName = createdUser.LastName,
                Address = createdUser.Address,
                Phone = createdUser.Phone,
                Birthdate = createdUser.Birthdate,
                Dni = createdUser.Dni,
                Email = createdUser.Email
            };
            return Ok(userReadDto);
        }

        /// <summary>
        /// Actualizar un usuario
        /// </summary>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Put(int id, [FromBody] UserResponseDto dtoUser)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var existingUser = await _userService.GetUserByIdAsync(id);
            if (!existingUser.Success)
            {
                return NotFound();
            }
            existingUser.Data.Name = dtoUser.Name;
            existingUser.Data.LastName = dtoUser.LastName;
            existingUser.Data.Address = dtoUser.Address;
            existingUser.Data.Phone = dtoUser.Phone;
            existingUser.Data.Birthdate = dtoUser.Birthdate;
            existingUser.Data.Dni = dtoUser.Dni;
            existingUser.Data.Email = dtoUser.Email;

            var response = await _userService.UpdateUserAsync(existingUser.Data);
            if (response.Success)
            {
                return Ok(response.Data); 
            }

            if (response.Message.Contains("no encontrado"))
            {
                return NotFound();
            }
            if (response.Message.Contains("obligatorios") || response.Message.Contains("misma cédula"))
            {
                return BadRequest(response.Message);
            }
        
            return InternalServerError(new Exception(response.Message));
        }

        /// <summary>
        /// Eliminar un usuario
        /// </summary>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            var response = await _userService.DeleteUserAsync(id);
            if (!response.Success)
            {
                if (response.Message.Contains("no encontrado"))
                    return NotFound();

                return BadRequest(response.Message);
            }
            return StatusCode(HttpStatusCode.NoContent);
        }

    }
}
