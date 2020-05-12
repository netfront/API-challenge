using GraphQL.Types;

namespace Amazing.Application._Queries
{
    public class PostQueries : ObjectGraphType
    {
        public PostQueries()
        {
            this.Field<BooleanGraphType>("get");
        }
    }
}