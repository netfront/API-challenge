using Amazing.Application.DTO.GraphTypes;
using Amazing.Application.Repositories;
using Amazing.Persistence.Models;
using GraphQL.Types;

namespace Amazing.Application._Queries
{
    public class BlogQueries : ObjectGraphType
    {
        private readonly IUnitOfWork _uow;

        public BlogQueries(IUnitOfWork uow)
        {
            this._uow = uow;
            this.Field<BlogGraphType>("get",
                arguments: new QueryArguments(new QueryArgument<NonNullGraphType<IntGraphType>> { Name = "blogId" }),
                resolve: this.Get);
        }

        public Blog Get(ResolveFieldContext<object> context)
            => this._uow.BlogRepository.Get(context.GetArgument<int>("blogId"));
    }
}