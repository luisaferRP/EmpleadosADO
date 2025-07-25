using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PruebaTecnicaEmpleados.Api.Models;
using PruebaTecnicaEmpleados.Api.Shared;

namespace PruebaTecnicaEmpleados.Api.Services.Interface
{
    public interface IUserService
    {
        Task<ServiceResponse<IEnumerable<User>>> GetAllUsersAsync(); 
        Task<ServiceResponse<User>> GetUserByIdAsync(int id);       
        Task<ServiceResponse<User>> AddUserAsync(User user);           
        Task<ServiceResponse<User>> UpdateUserAsync(User user);      
        Task<ServiceResponse<bool>> DeleteUserAsync(int id);
    }
}
