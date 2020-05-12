using GraphQL.Types;

namespace Amazing.Application.DTO.InputTypes
{
    public class LoginUserInputType : InputObjectGraphType
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public LoginUserInputType()
        {
            this.Field<NonNullGraphType<StringGraphType>>("email");
            this.Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}