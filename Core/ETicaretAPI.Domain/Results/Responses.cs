using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ETicaretAPI.Domain.Results
{
    public class Responses
    {
        public class BaseResponse
        {
            public BaseResponse(bool isSuccess, string message)
            {
                IsSuccess = isSuccess;
                Message = message;
            }

            public bool IsSuccess { get; set; }
            public string Message { get; set; }

        }

        public class GenericDataResponse<T> : BaseResponse
        {
            public GenericDataResponse(T data, bool isSuccess, string message) : base(isSuccess, message)
            {
                Data = data;
            }

            public GenericDataResponse(bool isSuccess, string message) : base(isSuccess, message)
            {
            }

            public T? Data { get; set; }
        }
    }
}
