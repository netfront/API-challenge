using GraphQL.Types;

namespace Amazing.Application._Mutations
{
    public class UserMutations : ObjectGraphType
    {
        public UserMutations()
        {
            this.Field<BooleanGraphType>("doSomething");
        }
    }
}