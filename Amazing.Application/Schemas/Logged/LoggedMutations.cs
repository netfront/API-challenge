using Amazing.Application._Mutations;
using GraphQL.Types;

namespace Amazing.Application.Schemas.Logged
{
    public class LoggedMutations : ObjectGraphType
    {
        public LoggedMutations(MutationCollection mutationCollection)
        {
            this.Field<UserMutations>("user", resolve: ctx => mutationCollection.UserMutation);
            this.Field<PostMutations>("post", resolve: ctx => mutationCollection.PostMutations);
            this.Field<BlogMutations>("blog", resolve: ctx => mutationCollection.BlogMutations);
            this.Field<ContentMutations>("content", resolve: ctx => mutationCollection.ContentMutations);
        }
    }
}