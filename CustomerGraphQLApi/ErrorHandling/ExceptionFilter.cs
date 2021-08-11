using HotChocolate;
using System;

namespace CustomerGraphQLApi.ErrorHandling
{
    public class ExceptionFilter : IErrorFilter
    {

        public IError OnError(IError error)
        {
            if (error.Exception is CustomerNotFoundException ex)
                return error.WithMessage($"Book with id {ex.CustomerId} not found");

            return error;
        }
    }
    public class CustomerNotFoundException : Exception
    {
        public long CustomerId { get; set; }
    }
}
