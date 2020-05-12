using Amazing.Application.DTO.GraphTypes;
using Amazing.Application.DTO.InputTypes;
using Amazing.Application.Extensions;
using Amazing.Application.Repositories;
using Amazing.Persistence.Models;
using GraphQL.Types;
using System;
using System.Net;

namespace Amazing.Application._Mutations
{
    public class BlogMutations : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;

        public BlogMutations(IUnitOfWork uow)
        {
            this._uow = uow;
            this.Field<BlogGraphType>("create",
                arguments: new QueryArguments(new QueryArgument<CreateBlogInputType> { Name = "request" }),
                resolve: this.Create);

            this.Field<BooleanGraphType>("delete",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "blogId" }),
                resolve: this.Delete);
        }

        /// <summary>
        /// Delete a specific blog
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public object Delete(ResolveFieldContext<object> context)
        {
            var userId = context.UserContext().GetUserIdFromBearer();

            var blogId = context.GetArgument<int>("blogId");

            this._uow.BlogRepository.Get(blogId)
                .WhenNull()
                .Throw(HttpStatusCode.NotFound, $"Blog not found")
                .WhenConditionFailed(c => c.UserId == userId)
                .Throw(HttpStatusCode.Unauthorized, $"Unauthorized");

            this._uow.BlogRepository.Delete(blogId);

            return true;
        }

        /// <summary>
        /// Create a blog
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Blog Create(ResolveFieldContext<object> context)
        {
            var request = context.GetArgument<CreateBlogInputType>("request");

            var userId = context.UserContext().GetUserIdFromBearer();

            var blog = new Blog
            {
                Name = request.Name,
                Url = request.Url,
                UserId = userId,
                CreationDate = DateTime.Now
            };
            this._uow.BlogRepository.Add(blog);

            return blog;
        }
    }
}