using Supabase.Postgrest.Attributes;
using Supabase.Postgrest.Models;

namespace DataGupe.Controllers;

[Table("ToDoLists")]
public class ToDoLists : BaseModel
{
    [PrimaryKey("id", false)]
    public int Id { get; set; }

    [Column("created_at")]
    public DateTime CreatedAt { get; set; }

    [Column("name")]
    public string Name { get; set; }
}