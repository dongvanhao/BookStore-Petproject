using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Results
{
    public class Result<T>
    {
        public bool IsSuccess { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }

        private Result(bool isSuccess, string message, T data)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }

        public static Result<T> Success(T data, string message = "")
            => new(true, message, data);

        public static Result<T> Failure(string message)
            => new(false, message, default);
    }
}

