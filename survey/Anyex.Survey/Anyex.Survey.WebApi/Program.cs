using Anyex.Survey.Application.DataAccess;
using Anyex.Survey.Application.Services.Survey;
using Anyex.Survey.Application.Services.SurveyResults;
using Anyex.Survey.Domain;
using Anyex.Survey.Infrastructure.DataAccess;
using Anyex.Survey.WebApi.Hubs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddSignalR();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


// DI services
builder.Services.AddScoped<ISurveyService, SurveyService>();
builder.Services.AddScoped<ISurveyResultsService, SurveyResultsService>();
builder.Services.AddScoped<IRepository<Survey, string>, FakeSurveyRepository>();


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

app.MapHub<SurveyResultsHub>("hubs/surveyResults");

app.Run();
