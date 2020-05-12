using Amazing.Application.Exceptions;
using System;
using System.Net;

namespace Amazing.Application.Extensions
{
    public static class ContractExtensions
    {
        public static T Throw<T>(this Contract<T> actual, HttpStatusCode statusCode, string message)
        {
            if (!actual.IsValid)
                throw new AmazingException(statusCode, message);
            return actual.Value;
        }
    }
}