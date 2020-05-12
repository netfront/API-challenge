using Amazing.Application.Schemas.Logged;
using Amazing.Application.Schemas.Public;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Amazing.Application.Schemas
{
    public interface ISchemaCollection
    {
        IPublicSchema PublicSchema { get; }
        ILoggedSchema LoggedSchema { get; }
    }

    public class SchemaCollection : ISchemaCollection
    {
        private readonly IServiceProvider _serviceProvider;

        public SchemaCollection(IServiceProvider serviceProvider)
        {
            this._serviceProvider = serviceProvider;
        }

        public IPublicSchema PublicSchema => this._serviceProvider.GetService<IPublicSchema>();
        public ILoggedSchema LoggedSchema => this._serviceProvider.GetService<ILoggedSchema>();
    }
}