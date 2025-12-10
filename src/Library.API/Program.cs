using Library.Application;
using Library.Application.Mappings;
using Library.Application.Services;
using Library.Application.Interfaces;
using Library.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
            policy
                .WithOrigins("http://localhost:5173") // puerto de Vite
                .AllowAnyHeader()
                .AllowAnyMethod());
});

// Infrastructure (DbContext, repos, UoW)
builder.Services.AddInfrastructure();

// AutoMapper
builder.Services.AddAutoMapper(typeof(MappingProfile).Assembly);

// Application Services
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILoanService, LoanService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowFrontend");
app.UseAuthorization();
app.MapControllers();

app.Run();
