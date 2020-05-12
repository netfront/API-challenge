using Microsoft.Extensions.DependencyInjection;
using System;

namespace Amazing.Application._Queries
{
    public interface IQueryCollection
    {
        UserQueries UserQueries { get; }
        PostQueries PostQueries { get; }
        BlogQueries BlogQueries { get; }
        ContentQueries ContentQueries { get; }
    }

    public class QueryCollection : IQueryCollection
    {
        private readonly IServiceProvider _serviceProvider;

        public QueryCollection(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public UserQueries UserQueries => this._serviceProvider.GetService<UserQueries>();
        public PostQueries PostQueries => this._serviceProvider.GetService<PostQueries>();
        public BlogQueries BlogQueries => this._serviceProvider.GetService<BlogQueries>();
        public ContentQueries ContentQueries => this._serviceProvider.GetService<ContentQueries>();
    }
}