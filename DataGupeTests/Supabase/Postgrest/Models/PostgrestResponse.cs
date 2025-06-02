using DataGupe.Controllers;
using Newtonsoft.Json;
using Supabase.Postgrest.Responses;

namespace Supabase.Postgrest.Models;
internal class PostgrestResponse<T> : ModeledResponse<ToDoLists>
{
    private Supabase.Postgrest.Responses.BaseResponse baseResponse; // Updated type to match the expected namespace  
    private JsonSerializerSettings jsonSerializerSettings;
    private object value;
    private bool v;

    public PostgrestResponse(Supabase.Postgrest.Responses.BaseResponse baseResponse, JsonSerializerSettings serializerSettings, Func<Dictionary<string, string>>? getHeaders = null, bool shouldParse = true) : base(baseResponse, serializerSettings, getHeaders, shouldParse)
    {
        Model = new List<ToDoLists>(); // Initialize Model to avoid CS8618    
    }

    public PostgrestResponse(Supabase.Postgrest.Responses.BaseResponse baseResponse, JsonSerializerSettings jsonSerializerSettings, object value, bool v) : base(baseResponse, jsonSerializerSettings, null, v)
    {
        this.baseResponse = baseResponse;
        this.jsonSerializerSettings = jsonSerializerSettings;
        this.value = value;
        this.v = v;
        Model = new List<ToDoLists>(); // Initialize Model to avoid CS8618    
    }

    public List<ToDoLists> Model { get; set; }
}
