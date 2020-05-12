using Amazing.Application._Queries;
using GraphQL.Types;

namespace Amazing.Application.Schemas.Logged
{
    public class LoggedQueries : ObjectGraphType
    {
        public LoggedQueries(QueryCollection queryCollection)
        {
            this.Field<UserQueries>("user", resolve: ctx => queryCollection.UserQueries);
            this.Field<PostQueries>("post", resolve: ctx => queryCollection.PostQueries);
            this.Field<BlogQueries>("blog", resolve: ctx => queryCollection.BlogQueries);
            this.Field<ContentQueries>("content", resolve: ctx => queryCollection.ContentQueries);
        }
    }
}