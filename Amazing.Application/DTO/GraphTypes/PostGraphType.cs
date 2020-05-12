using Amazing.Application.Repositories;
using Amazing.Persistence.Models;
using GraphQL.Types;
using System.Linq;

namespace Amazing.Application.DTO.GraphTypes
{
    public class PostGraphType : ObjectGraphType<Post>
    {
        public PostGraphType(IUnitOfWork uow)
        {
            this.Field(c => c.Id);
            this.Field(c => c.BlogId);
            this.Field(c => c.Title);
            this.Field<BlogGraphType>("blog", resolve: ctx => uow.BlogRepository.Get(ctx.Source.BlogId));
            this.Field<ListGraphType<ContentGraphType>>("contents", resolve: ctx => uow.ContentRepository.GetByPost(ctx.Source.Id).ToList());
        }
    }
}