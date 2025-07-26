using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI.WebControls.WebParts;
using PruebaTecnicaEmpleados.Api.Models;
using PruebaTecnicaEmpleados.Api.Repository.Interface;

namespace PruebaTecnicaEmpleados.Api.Repository.Implementations
{
    public class UserRepository : IUserRepository
    {

        private readonly string connectionString;

        public UserRepository()
        {
            connectionString = ConfigurationManager.ConnectionStrings["EmployeeDB"].ConnectionString;
        }

        //Mapeamos el objeto User desde el SqlDataReader,para poder convertir los datos de la base de datos en un objeto User.
        private User MapUser(SqlDataReader reader)
        {
            return new User
            {
                Id = (int)reader["Id"],
                Name = reader["Name"].ToString(),
                LastName = reader["LastName"].ToString(),
                Address = reader["Address"].ToString(),
                Phone = reader["Phone"].ToString(),
                Birthdate = (DateTime)reader["Birthdate"],
                Dni = reader["Dni"].ToString(),
                Email = reader["Email"].ToString()
            };
        }

        public async Task AddAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "INSERT INTO Users (Name, LastName, Address, Phone, Birthdate, Dni, Email) " +
                    "VALUES (@Name, @LastName, @Address, @Phone, @Birthdate, @Dni, @Email)", connection);

                command.Parameters.AddWithValue("@Name", user.Name);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Birthdate", user.Birthdate);
                command.Parameters.AddWithValue("@Dni", user.Dni);
                command.Parameters.AddWithValue("@Email", user.Email);
                await connection.OpenAsync();

                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("El usuario no se pudo agregar.");
                }

            }
        }

        public async Task DeleteAsync(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("DELETE FROM Users WHERE Id = @Id ", connection);
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("El usuario no se pudo eliminar.");
                }
            }
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            var users = new List<User>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users", connection);
                await connection.OpenAsync();
                using (SqlDataReader reader =  command.ExecuteReader())
                {
                    while (await reader.ReadAsync())
                    {
                        users.Add(MapUser(reader));
                    }
                }
            }
            return users.OrderBy(u => u.Name).ToList(); 
        }

        public async Task<User> GetByIdAsync(int id)
        {
            User user = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand("SELECT * FROM Users WHERE Id = @Id", connection);
                command.Parameters.AddWithValue("@Id", id);
                await connection.OpenAsync();

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (await reader.ReadAsync())
                    {
                        user = MapUser(reader);
                    }
                } 
            }
            return user;
        }

        public async Task UpdateAsync(User user)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(
                    "UPDATE Users SET Name = @Name, LastName = @LastName, Address = @Address, " +
                    "Phone = @Phone, Birthdate = @Birthdate, Dni = @Dni, Email = @Email WHERE Id = @Id", connection);

                command.Parameters.AddWithValue("@Name",user.Name);
                command.Parameters.AddWithValue("@LastName", user.LastName);
                command.Parameters.AddWithValue("@Address", user.Address);
                command.Parameters.AddWithValue("@Phone", user.Phone);
                command.Parameters.AddWithValue("@Birthdate", user.Birthdate);
                command.Parameters.AddWithValue("@Dni", user.Dni);
                command.Parameters.AddWithValue("@Email", user.Email);
                command.Parameters.AddWithValue("@Id", user.Id);
                await connection.OpenAsync();
                int rowsAffected = await command.ExecuteNonQueryAsync();
                if (rowsAffected == 0)
                {
                    throw new Exception("El usuario no se pudo actualizar.");
                }
            }
        }

        public async Task<bool> ExistsByCedulaAsync(string dni, int? excludeId = null)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string command = "SELECT COUNT(*) FROM Users WHERE Dni = @Dni";
                if (excludeId.HasValue)
                {
                    command += " AND Id != @ExcludeId";
                }
                SqlCommand cmd = new SqlCommand(command, connection);
                cmd.Parameters.AddWithValue("@Dni", dni);
                if (excludeId.HasValue)
                {
                    cmd.Parameters.AddWithValue("@ExcludeId", excludeId.Value);
                }

                await connection.OpenAsync();
                int count = (int)await cmd.ExecuteScalarAsync();
                return count > 0;
            }
        }
    }
}