using System;
using System.Collections.Generic;
using System.Text;

namespace Business.common
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public T Data { get; set; }

        private Result(bool success, string message, T data)
        {
            Success = success;
            Message = message;
            Data = data;
        }

        public static Result<T> Ok()
        {
            return new Result<T>(true, null, default);
        }

        public static Result<T> Ok(T data)
        {
            return new Result<T>(true, null, data);
        }

        public static Result<T> Error(string message)
        {
            return new Result<T>(false, message, default);
        }
    }
}
