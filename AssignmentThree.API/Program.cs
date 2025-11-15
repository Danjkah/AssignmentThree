using Microsoft.EntityFrameworkCore;
using AssignmentThree.Core.Interfaces;
using AssignmentThree.Infrastructure.Data;
using AssignmentThree.Infrastructure.Repositories;




var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers().AddNewtonsoftJson(); // Required for JSON Patch support

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


// Configure SQLite Database
builder.Services.AddDbContext<AssignmentDbContext>(options =>
options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection")));


// Register Repository
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseAuthorization();
app.MapControllers();

app.Run();


