using DataGupe.Controllers;
using Newtonsoft.Json;
using Supabase.Postgrest.Responses;

namespace DataGupe.Tests.Controllers;
internal class PostgrestResponse<T> : ModeledResponse<ToDoLists>
{
    public PostgrestResponse(BaseResponse baseResponse, JsonSerializerSettings serializerSettings, Func<Dictionary<string, string>>? getHeaders = null, bool shouldParse = true) : base(baseResponse, serializerSettings, getHeaders, shouldParse)
    {
    }

    public List<ToDoLists> Model => new List<ToDoLists>
    {
        new ToDoLists { Id = 1, CreatedAt = DateTime.UtcNow, Name = "Test Todo" },
        new ToDoLists { Id = 2, CreatedAt = DateTime.UtcNow, Name = "Another Todo" }
    };
}