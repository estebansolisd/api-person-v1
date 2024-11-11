using MediatR;
using Microsoft.EntityFrameworkCore;
using person_api_1.Commands;
using person_api_1.Data;
using person_api_1.Handlers;
using person_api_1.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<Program>());
// builder.Services.AddMediatR(typeof(Program).Assembly); // Register MediatR if used for CQRS
 
builder.Services.AddDbContext<DatabaseContext>(options =>
    options.UseInMemoryDatabase("PersonDb"));
builder.Services.AddScoped<IPersonRepository, PersonRepository>();

// Register the command handler
builder.Services.AddScoped<IRequestHandler<AddPersonCommand, Person>, AddPersonHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
