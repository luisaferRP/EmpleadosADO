using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PruebaTecnicaEmpleados.Api.Models;

namespace PruebaTecnicaEmpleados.Api.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
    }
}
