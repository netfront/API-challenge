using GraphQL;
using GraphQL.Types;

namespace Amazing.Application.Schemas.Public
{
    public interface IPublicSchema : ISchema
    { }

    public class PublicSchema : Schema, IPublicSchema
    {
        public PublicSchema(IDependencyResolver resolver) : base(resolver)
        {
            this.Query = resolver.Resolve<PublicQueries>();
            this.Mutation = resolver.Resolve<PublicMutations>();
        }
    }
}