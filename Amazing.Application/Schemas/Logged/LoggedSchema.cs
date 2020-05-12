using GraphQL;
using GraphQL.Types;

namespace Amazing.Application.Schemas.Logged
{
    public interface ILoggedSchema : ISchema
    {
    }

    public class LoggedSchema : Schema, ILoggedSchema
    {
        public LoggedSchema(IDependencyResolver resolver) : base(resolver)
        {
            this.Query = resolver.Resolve<LoggedQueries>();
            this.Mutation = resolver.Resolve<LoggedMutations>();
        }
    }
}