using GraphQL.Types;
using System;

namespace Amazing.Application.DTO.InputTypes
{
    public class CreatePostInputType : InputObjectGraphType
    {
        public string Title { get; set; }
        public DateTime ReleaseDateTime { get; set; }
        public int BlogId { get; set; }

        public CreatePostInputType()
        {
            this.Field<NonNullGraphType<StringGraphType>>("title");
            this.Field<NonNullGraphType<DateTimeGraphType>>("releaseDateTime");
            this.Field<NonNullGraphType<IntGraphType>>("blogId");
        }
    }
}