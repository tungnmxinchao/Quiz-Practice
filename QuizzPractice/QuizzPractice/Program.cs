using System.Security.Claims;
using System.Text;
using System.Text.Json.Serialization;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
builder.Services.AddTransient<ISubjectService, SubjectService>();
builder.Services.AddTransient<IQuestionService, QuestionService>();
builder.Services.AddTransient<IResultService, ResultService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IOptionService, OptionService>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
});

var key = Encoding.ASCII.GetBytes(builder.Configuration["Jwt:Key"]);
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        RoleClaimType = ClaimTypes.Role,
        ValidateAudience = false
    };
});


builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Teacher", policy => policy.RequireRole("teacher"));
    options.AddPolicy("Student", policy => policy.RequireRole("student"));

    options.AddPolicy("TeacherOrStudent", policy =>
        policy.RequireRole("teacher", "student"));
});

builder.Services.AddControllers();
builder.Services.AddDbContext<QuizDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DB"))
    .EnableSensitiveDataLogging()
    );

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
app.UseCors("AllowAll");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static IEdmModel GetEdmModel()
{
    var builder = new ODataConventionModelBuilder();

    var quizzes = builder.EntitySet<GetQuizResponse>("Quiz").EntityType;
    quizzes.HasKey(u => u.QuizId);

    var subjects = builder.EntitySet<GetSubjectResponse>("Subject").EntityType;
    subjects.HasKey(k => k.SubjectId);

    var questions = builder.EntitySet<GetQuestionResponse>("Question").EntityType;
    questions.HasKey(k => k.QuestionId);

    var results = builder.EntitySet<GetResultsResponse>("Result").EntityType;
    results.HasKey(r => r.ResultId);

    var users = builder.EntitySet<GetUserResponse>("User").EntityType;
    users.HasKey(u => u.UserId);

    return builder.GetEdmModel();
}
