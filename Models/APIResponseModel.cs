using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class APIResponseModel
    {
        public bool IsSuccess { get; set; } = true;
        public string Message { get; set; } = "Successfully executed.";
        public object? Data { get; set; }

        public static APIResponseModel Success()
        {
            return new APIResponseModel
            {
                IsSuccess = true
            };
        }

        public static APIResponseModel Success(string message)
        {
            return new APIResponseModel
            {
                Message = message,
                IsSuccess = true
            };
        }

        public static APIResponseModel Success(object data)
        {
            return new APIResponseModel
            {
                IsSuccess = true,
                Data = data
            };
        }

        public static APIResponseModel Failure()
        {
            return new APIResponseModel
            {
                Message = "Error while implementing.",
                IsSuccess = false
            };
        }

        public static APIResponseModel Failure(string message)
        {
            return new APIResponseModel
            {
                Message = message,
                IsSuccess = false
            };
        }
    }
}
