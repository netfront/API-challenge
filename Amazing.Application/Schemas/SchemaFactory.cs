using Amazing.Application.Context;
using Amazing.Application.Extensions;
using Amazing.Application.Repositories;
using Amazing.Application.Security;
using Amazing.Persistence.Enumerators;
using GraphQL.Types;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;

namespace Amazing.Application.Schemas
{
    public interface ISchemaFactory
    {
        ISchemaFactory WithHttpRequest(HttpRequest request);

        ISchemaFactory DefinePolicy();

        ISchema ProvideSchema();

        AmazingRequestContext ProvideContext();

        EPolicyType Type { get; }
    }

    public class SchemaFactory : ISchemaFactory
    {
        private HttpRequest httpRequest { get; set; }
        private ISchemaCollection _SchemaCollection { get; set; }
        private IUnitOfWork _unitOfWork { get; }
        private IConfiguration _configuration { get; }
        public EPolicyType Type { get; private set; }
        private IJsonWebTokenValidator _jsonWebTokenValidator { get; }
        public AmazingRequestContext RequestContext { get; private set; }

        public SchemaFactory(ISchemaCollection schemaCollection, IUnitOfWork unitOfWork, IConfiguration config, IJsonWebTokenValidator jsonWebTokenValidator)
        {
            this.RequestContext = new AmazingRequestContext();
            this._jsonWebTokenValidator = jsonWebTokenValidator;
            this._SchemaCollection = schemaCollection;
            this._unitOfWork = unitOfWork;
            this._configuration = config;
        }

        public ISchemaFactory WithHttpRequest(HttpRequest request)
        {
            this.httpRequest = request;
            return this;
        }

        /// <summary>
        /// Define the policy of the request
        /// </summary>
        /// <returns></returns>
        public ISchemaFactory DefinePolicy()
        {
            var authorization = this.httpRequest.GetHeaderValue("authorization");

            if (!string.IsNullOrEmpty(authorization))
            {
                var claims = this._jsonWebTokenValidator.ValidateAndDecode(authorization);
                this.RequestContext.WithUserDetail(claims).WithAuthorization(authorization);
                this.Type = EPolicyType.Logged;
            }
            else
            {
                this.Type = EPolicyType.Public;
            }

            return this;
        }

        public AmazingRequestContext ProvideContext() => this.RequestContext;

        /// <summary>
        /// Provide the schema
        /// </summary>
        /// <returns></returns>
        public ISchema ProvideSchema()
            => this.Type switch
            {
                EPolicyType.Unknown => (ISchema)null,
                EPolicyType.Public => this._SchemaCollection.PublicSchema,
                EPolicyType.Logged => this._SchemaCollection.LoggedSchema,
                _ => throw new ArgumentOutOfRangeException()
            };
    }
}