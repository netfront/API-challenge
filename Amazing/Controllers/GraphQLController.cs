using Amazing.API.DTO;
using Amazing.Application.Exceptions;
using Amazing.Application.Schemas;
using Amazing.Persistence.Enumerators;
using GraphQL;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Amazing.API.Controllers
{
    [Route("[controller]")]
    public class GraphQLController : Controller
    {
        private readonly IDocumentExecuter _documentExecuter;
        private readonly ISchemaFactory _schemaFactory;

        public GraphQLController(IDocumentExecuter documentExecuter, ISchemaFactory schemaFactory)
        {
            this._documentExecuter = documentExecuter;
            this._schemaFactory = schemaFactory;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GraphQLQuery query)
        {
            this._schemaFactory.WithHttpRequest(this.Request)
                .DefinePolicy();

            if (this._schemaFactory.Type == EPolicyType.Unknown)
                return this.BadRequest();

            var executionOptions = new ExecutionOptions
            {
                Schema = this._schemaFactory.ProvideSchema(),
                Query = query.Query,
                Inputs = query.Variables.ToInputs(),
                UserContext = this._schemaFactory.ProvideContext(),
            };

            var result = await this._documentExecuter.ExecuteAsync(executionOptions).ConfigureAwait(false);

            if (result.Errors?.Count > 0)
            {
                var response = new GraphQLError
                {
                    Message = "Internal server error: ",
                    Code = 500
                };
                result.Errors?.ForEach(c =>
                {
                    if (c.InnerException != null && c.InnerException is AmazingException exception)
                    {
                        response.Message = exception.Message;
                        response.Code = (int)exception.Status;
                    }
                    else if (c.InnerException != null)
                    {
                        response.Message = c.InnerException.Message;
                    }
                    else
                        response.Message += c.Message;
                });

                return Ok(new
                {
                    errors = new List<GraphQLError> { response }
                });
            }

            return this.Ok(result);
        }
    }
}