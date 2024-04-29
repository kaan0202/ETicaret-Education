using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ETicaretAPI.Domain.Results.Responses;

namespace ETicaretAPI.Domain.Results
{
    public static class ResponseFactory
    {
        public static BaseResponse SuccessResponse(string message) => new(true, message);
        public static BaseResponse FailResponse(string message) => new(false, message);
        public static GenericDataResponse<T> SuccessResponse<T>(T data, string message) => new(data, true, message);
        public static GenericDataResponse<T> FailResponse<T>(T data, string message) => new(data, false, message);
        public static GenericDataResponse<T> FailResponse<T>(string message) => new(false, message);
    }
}
