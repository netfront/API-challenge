using Amazing.Persistence.Models;
using GraphQL.Types;

namespace Amazing.Application.DTO.GraphTypes
{
    public class UserGraphType : ObjectGraphType<User>
    {
        public UserGraphType()
        {
            this.Field(c => c.Id);
            this.Field(c => c.Firstname);
            this.Field(c => c.Lastname);
            this.Field(c => c.Email);
        }
    }
}