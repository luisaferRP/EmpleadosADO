using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.Extensions.Logging;
using PruebaTecnicaEmpleados.Api.Models;
using PruebaTecnicaEmpleados.Api.Repository.Interface;
using PruebaTecnicaEmpleados.Api.Services.Interface;
using PruebaTecnicaEmpleados.Api.Shared;

namespace PruebaTecnicaEmpleados.Api.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly ILogger<UserService> _logger;

        public UserService(IUserRepository userRepository, Ilogger<UserService> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        public async Task<ServiceResponse<User>> AddUserAsync(User user)
        {
            if (user == null)
            {
                return ServiceResponse<User>.Fail("El usuario no puede ser nulo");
            }
            if(string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.LastName) || string.IsNullOrEmpty(user.Email))
            {
                _logger.LogWarning("Datos incompletos para el usuario.");
                return ServiceResponse<User>.Fail("El nombre, apellido y correo electrónico son obligatorios");
            }

            if (await _userRepository.ExistsByCedulaAsync(user.Dni))
            {
                return ServiceResponse<User>.Fail("Ya existe un usuario con la misma cédula");
            }

            try
            {
                await _userRepository.AddAsync(user);
                _logger.LogInformation($"Usuario creado con éxito: ID {createdUser.Id}");
                return ServiceResponse<User>.Ok(user, "Usuario creado exitosamente");
            }
            catch(Exception ex)
            {
                return ServiceResponse<User>.Fail("Error al crear el usuario: " + ex.Message);
            }
        }

        public async Task<ServiceResponse<bool>> DeleteUserAsync(int id)
        {
            var existingUser = await GetUserByIdAsync(id);
            if(existingUser.Data == null)
            {
                return ServiceResponse<bool>.Fail("Usuario no encontrado");
            }
            try
            {
                await _userRepository.DeleteAsync(id);
                return ServiceResponse<bool>.Ok(true, "Usuario eliminado de forma exitosa");
            }
            catch (Exception ex)
            {
                return ServiceResponse<bool>.Fail($"Error al eliminar usuario : {ex.Message}");
            }
        }

        public async Task<ServiceResponse<IEnumerable<User>>> GetAllUsersAsync()
        {
            try
            {
                var users = await _userRepository.GetAllAsync();
                return ServiceResponse<IEnumerable<User>>.Ok(users, "Usuarios obtenidos exitosamente");
            }
            catch (Exception ex)
            {
                return ServiceResponse<IEnumerable<User>>.Fail("Error al obtener los usuarios: " + ex.Message);
            }
        }

        public async Task<ServiceResponse<User>> GetUserByIdAsync(int id)
        {
            try
            {
                var user = await _userRepository.GetByIdAsync(id);
                if (user == null)
                {
                    return ServiceResponse<User>.Fail($"Usuario con Id {id} no encontrado");
                }
                return ServiceResponse<User>.Ok(user);
            }
            catch(Exception ex)
            { 
                return ServiceResponse<User>.Fail($"Error interno al obtener el usuario: {ex.Message}");
            }
        }

        public async Task<ServiceResponse<User>> UpdateUserAsync(User user)
        {
            if (user == null || string.IsNullOrWhiteSpace(user.Name) || string.IsNullOrWhiteSpace(user.LastName) || string.IsNullOrWhiteSpace(user.Email))
            {
                return ServiceResponse<User>.Fail("El nombre, apellido y correo electrónico son obligatorios");
            }

            var existingUser = await _userRepository.GetByIdAsync(user.Id);
            if (existingUser == null)
            {
                return ServiceResponse<User>.Fail("El usuario no se ha podido actualizar o no se encuentra");
            }
            if (await _userRepository.ExistsByCedulaAsync(user.Dni,user.Id))
            {
                return ServiceResponse<User>.Fail("Ya existe otro usuario con la misma cédula.");
            }
            try
            {
                await _userRepository.UpdateAsync(user);
                return ServiceResponse<User>.Ok(user,"Usuario actualizado de forma exitosa");
            }
            catch (Exception ex)
            {
                return ServiceResponse<User>.Fail($"Error interno al actualizar usuario: {ex.Message}");
            }
        }
    }
}