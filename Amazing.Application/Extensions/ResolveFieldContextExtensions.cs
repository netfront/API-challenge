using Amazing.Application.Context;
using GraphQL.Types;

namespace Amazing.Application.Extensions
{
    public static class ResolveFieldContextExtensions
    {
        public static AmazingRequestContext UserContext(this ResolveFieldContext<object> ctx)
            => ctx.UserContext as AmazingRequestContext;
    }
}