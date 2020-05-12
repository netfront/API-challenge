using GraphQL.Types;

namespace Amazing.Application.DTO.InputTypes
{
    public class CreateContentInputType : InputObjectGraphType
    {
        public string Text { get; set; }
        public int PostId { get; set; }

        public CreateContentInputType()
        {
            this.Field<NonNullGraphType<IntGraphType>>("postId");
            this.Field<NonNullGraphType<StringGraphType>>("text");
        }
    }
}