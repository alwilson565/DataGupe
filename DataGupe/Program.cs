var builder = WebApplication.CreateBuilder(args);

// Use Stashbox as the DI container
builder.Host.UseStashbox();

// Add services to the container.
builder.Services.AddSingleton<Supabase.Client>(provider =>
{
    var supabaseUrl = "https://your-supabase-url.supabase.co";
    var supabaseKey = "your-anon-key";
    var client = new Supabase.Client(supabaseUrl, supabaseKey);
    client.InitializeAsync().GetAwaiter().GetResult();
    return client;
});

// Register your repository
builder.Services.AddScoped<ITodoRepository, TodoRepository>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
