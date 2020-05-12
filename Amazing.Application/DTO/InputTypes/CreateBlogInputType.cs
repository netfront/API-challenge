using GraphQL.Types;

namespace Amazing.Application.DTO.InputTypes
{
    public class CreateBlogInputType : InputObjectGraphType
    {
        public string Name { get; set; }
        public string Url { get; set; }

        public CreateBlogInputType()
        {
            this.Field<NonNullGraphType<StringGraphType>>("name");
            this.Field<NonNullGraphType<StringGraphType>>("url");
        }
    }
}