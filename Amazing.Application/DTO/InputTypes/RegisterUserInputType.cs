using GraphQL.Types;

namespace Amazing.Application.DTO.InputTypes
{
    public class RegisterUserInputType : InputObjectGraphType
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public RegisterUserInputType()
        {
            this.Field<NonNullGraphType<StringGraphType>>("firstname");
            this.Field<NonNullGraphType<StringGraphType>>("lastname");
            this.Field<NonNullGraphType<StringGraphType>>("email");
            this.Field<NonNullGraphType<StringGraphType>>("password");
        }
    }
}