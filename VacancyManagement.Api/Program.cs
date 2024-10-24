using Microsoft.EntityFrameworkCore;
using MediatR; // Add this line for MediatR
using VacancyManagement.Infrastructure;
using VacancyManagement.Application.Questions.Queries;
using System.Text.Json.Serialization;
using VacancyManagement.Application.Services;

var builder = WebApplication.CreateBuilder(args);

// Register ApplicationDbContext with SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register MediatR
builder.Services.AddMediatR(typeof(GetQuestionsQuery).Assembly);
builder.Services.AddScoped<IFileUploadService, FileUploadService>();

// Also register the IFileStorage service (if not already done)

// Configure JSON serialization options to handle circular references
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;

        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
    });


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
