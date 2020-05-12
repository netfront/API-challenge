using GraphQL.Types;

namespace Amazing.Application._Queries
{
    public class UserQueries : ObjectGraphType
    {
        public UserQueries()
        {
            this.Field<BooleanGraphType>("getUser");
        }
    }
}