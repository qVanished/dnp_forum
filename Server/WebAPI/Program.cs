using EfcRepositories;
using FileRepositories;
using RepositoryContracts;
using EfcRepositories;
using AppContext = EfcRepositories.AppContext;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
// builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IPostRepository, EFCPostRepository>();
builder.Services.AddScoped<IUserRepository, EFCUserRepository>();
builder.Services.AddScoped<ICommentRepository, EFCCommentRepository>();
builder.Services.AddDbContext<AppContext>();

var app = builder.Build();

app.MapControllers();

//Configure the HTTP request pipeline.

// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();

// }

app.UseHttpsRedirection();

Console.WriteLine("API is running");
app.Run();

