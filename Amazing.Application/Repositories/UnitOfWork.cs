using Microsoft.Extensions.DependencyInjection;
using System;

namespace Amazing.Application.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBlogRepository BlogRepository { get; }
        public IContentRepository ContentRepository { get; }
        public IPostRepository PostRepository { get; }
    }

    public class UnitOfWork : IUnitOfWork
    {
        private readonly IServiceProvider _serviceProvider;

        public UnitOfWork(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IUserRepository UserRepository => this._serviceProvider.GetRequiredService<IUserRepository>();
        public IBlogRepository BlogRepository => this._serviceProvider.GetRequiredService<IBlogRepository>();
        public IContentRepository ContentRepository => this._serviceProvider.GetRequiredService<IContentRepository>();
        public IPostRepository PostRepository => this._serviceProvider.GetRequiredService<IPostRepository>();
    }
}