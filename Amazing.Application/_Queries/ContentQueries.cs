using GraphQL.Types;

namespace Amazing.Application._Queries
{
    public class ContentQueries : ObjectGraphType
    {
        public ContentQueries()
        {
            this.Field<BooleanGraphType>("get");
        }
    }
}