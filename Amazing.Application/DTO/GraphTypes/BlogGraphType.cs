using Amazing.Application.Repositories;
using Amazing.Persistence.Models;
using GraphQL.Types;
using System.Linq;

namespace Amazing.Application.DTO.GraphTypes
{
    public class BlogGraphType : ObjectGraphType<Blog>
    {
        public BlogGraphType(IUnitOfWork uow)
        {
            this.Field(c => c.Id);
            this.Field(c => c.Url);
            this.Field(c => c.Name);
            this.Field(c => c.UserId);
            this.Field<ListGraphType<PostGraphType>>("posts", resolve: ctx => uow.PostRepository.GetByBlog(ctx.Source.Id).ToList());
            this.Field<UserGraphType>("user", resolve: c => uow.UserRepository.Get(c.Source.UserId));
        }
    }
}