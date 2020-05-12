using Amazing.Application.DTO.InputTypes;
using Amazing.Application.Extensions;
using Amazing.Application.Repositories;
using Amazing.Persistence.Models;
using GraphQL.Types;
using System;
using System.Net;

namespace Amazing.Application.Schemas.Public
{
    public class PublicMutations : ObjectGraphType
    {
        private readonly IUnitOfWork _unitOfWork;

        public PublicMutations(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this.Field<BooleanGraphType>("registerUser",
                arguments: new QueryArguments(new QueryArgument<RegisterUserInputType> { Name = "request" }),
                resolve: Register);
        }

        public object Register(ResolveFieldContext<object> context)
        {
            var request = context.GetArgument<RegisterUserInputType>("request");

            this._unitOfWork.UserRepository.Get(request.Email)
                .WhenConditionFailed(c => c == null)
                .Throw(HttpStatusCode.BadRequest, $"Email already used");

            this._unitOfWork.UserRepository.Add(new User
            {
                Email = request.Email,
                CreationDate = DateTime.Now,
                Firstname = request.Firstname,
                Lastname = request.Lastname,
                Password = request.Password
            });

            return true;
        }
    }
}