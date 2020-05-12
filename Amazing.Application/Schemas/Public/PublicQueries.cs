using Amazing.Application.DTO.InputTypes;
using Amazing.Application.Extensions;
using Amazing.Application.Repositories;
using Amazing.Application.Security;
using Amazing.Persistence.Enumerators;
using GraphQL.Types;
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Claims;

namespace Amazing.Application.Schemas.Public
{
    public class PublicQueries : ObjectGraphType
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJsonWebTokenProvider _tokenProvider;

        public PublicQueries(IUnitOfWork unitOfWork, IJsonWebTokenProvider tokenProvider)
        {
            this._unitOfWork = unitOfWork;
            this._tokenProvider = tokenProvider;

            this.Field<StringGraphType>("login",
                arguments: new QueryArguments(new QueryArgument<LoginUserInputType> { Name = "request" }),
                resolve: Login);
        }

        public object Login(ResolveFieldContext<object> context)
        {
            var request = context.GetArgument<LoginUserInputType>("request");

            var user = this._unitOfWork.UserRepository.Get(request.Email, request.Password)
                .WhenNull()
                .Throw(HttpStatusCode.NotFound, $"User not found");

            var token = this._tokenProvider.Generate(1, new List<Claim>
            {
                new Claim(ClaimTypes.UserData, user.Id.ToString())
            }, EJwtType.ConnectedUser);

            return token;
        }
    }
}