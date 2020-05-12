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
    public class ContentMutations : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;

        public ContentMutations(IUnitOfWork uow)
        {
            this._uow = uow;
            this.Field<ContentGraphType>("create",
                arguments: new QueryArguments(new QueryArgument<CreateContentInputType> { Name = "request" }),
            resolve: this.Create);
        }

        /// <summary>
        /// Create a content
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Content Create(ResolveFieldContext<object> context)
        {
            var userId = context.UserContext().GetUserIdFromBearer();

            var request = context.GetArgument<CreateContentInputType>("request");

            var post = this._uow.PostRepository.Get(request.PostId, "Contents")
                .WhenNull()
                .Throw(HttpStatusCode.NotFound, $"Post not found");

            var content = new Content
            {
                Text = request.Text,
                PostId = request.PostId,
                Sort = post.Contents.Count,
                CreationDate = DateTime.Now
            };

            this._uow.ContentRepository.Add(content);

            return content;
        }
    }
}