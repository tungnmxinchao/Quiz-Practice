using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OData.Edm;
using Microsoft.OData.ModelBuilder;
using QuizzPractice.Db;
using QuizzPractice.DTOs.Response;
using QuizzPractice.Interface;
using QuizzPractice.Mapper;
using QuizzPractice.Service;

var builder = WebApplication.CreateBuilder(args);

var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MapperConfig()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


builder.Services.AddTransient<IQuizService, QuizService>();

builder.Services.AddControllers();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB")));

builder.Services.AddControllers()
    .AddOData(opt => opt
                .Select()
                .Filter()
                .OrderBy()
                .Expand()
                .Count()
                .SetMaxTop(100)
                .AddRouteComponents("odata", GetEdmModel())
            )
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
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

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    var quizzes = builder.EntitySet<GetQuizResponse>("Quiz").EntityType;

    quizzes.HasKey(u => u.QuizId);

    return builder.GetEdmModel();
}
