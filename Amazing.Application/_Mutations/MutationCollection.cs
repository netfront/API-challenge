using Microsoft.Extensions.DependencyInjection;
using System;

namespace Amazing.Application._Mutations
{
    public interface IMutationCollection
    {
        UserMutations UserMutation { get; }
        PostMutations PostMutations { get; }
        BlogMutations BlogMutations { get; }
        ContentMutations ContentMutations { get; }
    }

    public class MutationCollection : IMutationCollection
    {
        private readonly IServiceProvider _serviceProvider;

        public MutationCollection(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public UserMutations UserMutation => this._serviceProvider.GetService<UserMutations>();
        public PostMutations PostMutations => this._serviceProvider.GetService<PostMutations>();
        public BlogMutations BlogMutations => this._serviceProvider.GetService<BlogMutations>();
        public ContentMutations ContentMutations => this._serviceProvider.GetService<ContentMutations>();
    }
}