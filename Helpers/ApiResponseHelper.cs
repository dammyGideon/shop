

using ShopCart.Dtos;

namespace ShopCart.Helpers
{
    public static class ApiResponseHelper
    {
        public static ApiResponse<T> Success <T>(string message) {

            return new ApiResponse<T>
            {
                IsSuccessful = true,
                Message = message,
                Data = default!
            };
        }

        public static ApiResponse<T> DataResponse <T>(T data, string message = "")
        {
            return new ApiResponse<T> { IsSuccessful = true, Message = message, Data = data };
        }

        public static ApiResponse <T> Failure <T>(string message)
        {
            return new ApiResponse<T>
            {
                IsSuccessful = true,
                Message = message,
                Data = default!

            };
        }

    }
}
