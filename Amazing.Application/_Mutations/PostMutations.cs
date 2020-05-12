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
    public class PostMutations : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;

        public PostMutations(IUnitOfWork uow)
        {
            this._uow = uow;
            this.Field<PostGraphType>("create",
                arguments: new QueryArguments(new QueryArgument<CreatePostInputType> { Name = "request" }),
                resolve: this.Create);
        }

        /// <summary>
        /// Create a post within a blog
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Post Create(ResolveFieldContext<object> context)
        {
            var userId = context.UserContext().GetUserIdFromBearer();

            var request = context.GetArgument<CreatePostInputType>("request");

            this._uow.BlogRepository.Get(request.BlogId)
                .WhenNull()
                .Throw(HttpStatusCode.BadRequest, $"Blog not found")
                .WhenConditionFailed(c => c.UserId == userId)
                .Throw(HttpStatusCode.Unauthorized, $"Unauthorized");

            var post = new Post
            {
                CreationDate = DateTime.Now,
                BlogId = request.BlogId,
                Title = request.Title,
                ReleaseDateTime = request.ReleaseDateTime,
            };

            this._uow.PostRepository.Add(post);
            return post;
        }
    }
}