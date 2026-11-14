using TodoApi;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ToDoDbContext>();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors();

app.MapGet("/", () => "Hello World!");


app.MapGet("/tasks",async (ToDoDbContext context) =>
{
    return await context.Items.ToListAsync();
});

app.MapPost("/new",async (ToDoDbContext context,Item item)=>{
    context.Items.Add(item);
    await context.SaveChangesAsync();
    return Results.Created($"/tasks/{item.Id}", item);
});

app.MapPut("/items/{id}", async (ToDoDbContext context, int id, Item updatedItem) =>
{
    var item = await context.Items.FirstOrDefaultAsync(i => i.Id == id);
    if (item == null) return Results.NotFound();
    item.IsComplete = updatedItem.IsComplete;
    await context.SaveChangesAsync();
    return Results.Ok(item);
});

app.MapDelete("/delete/{id}",async (ToDoDbContext context,int id)=>{
    Item? item=await context.Items.FirstOrDefaultAsync(i => i.Id == id);
    if(item==null)return Results.NotFound();
    context.Items.Remove(item);
    await context.SaveChangesAsync();
    return Results.NoContent();
});


app.Run();