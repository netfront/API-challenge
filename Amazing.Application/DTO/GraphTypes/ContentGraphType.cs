using Amazing.Persistence.Models;
using GraphQL.Types;

namespace Amazing.Application.DTO.GraphTypes
{
    public class ContentGraphType : ObjectGraphType<Content>
    {
        public ContentGraphType()
        {
            this.Field(c => c.Id);
            this.Field(c => c.PostId);
            this.Field(c => c.Sort);
            this.Field(c => c.Text);
        }
    }
}