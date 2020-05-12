using Newtonsoft.Json.Linq;

namespace Amazing.API
{
    public class GraphQLQuery
    {
        public GraphQLQuery(string operationName, string namedQuery, string query, JObject variables)
        {
            this.OperationName = operationName;
            this.NamedQuery = namedQuery;
            this.Query = query;
            this.Variables = variables;
        }

        public readonly string OperationName;
        public readonly string NamedQuery;
        public readonly string Query;
        public readonly JObject Variables;
    }
}