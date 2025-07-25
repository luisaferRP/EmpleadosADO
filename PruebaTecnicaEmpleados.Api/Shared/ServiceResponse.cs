using System;
using System.Collections.Generic;

namespace PruebaTecnicaEmpleados.Api.Shared
{
    public class ServiceResponse<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = string.Empty;
        public T Data { get; set; }

        public static ServiceResponse<T> Ok(T data, string message = "") =>
            new ServiceResponse<T> { Data = data, Message = message };
        //public static ServiceResponse<T> Ok(T data, string message = "") =>
        // new ServiceResponse<T> { Data = data, Message = message , Success= true};

        public static ServiceResponse<T> Fail(string message) =>
         new ServiceResponse<T> { Success = false, Message = message };
        //public static ServiceResponse<T> Fail(string message) =>
        //    new ServiceResponse<T> { Success = false, Message = message , Data = default};
    }
}