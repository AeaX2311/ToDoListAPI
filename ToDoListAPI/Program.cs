using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;
using ToDoListAPI.Attributes;
using ToDoListAPI.Data;
using ToDoListAPI.Middlewares;
using ToDoListAPI.Repositories.BusinessLogic.Task;
using ToDoListAPI.Repositories.BusinessLogic.User;
using ToDoListAPI.Services.Implementations;
using ToDoListAPI.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

object value = builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddControllers().ConfigureApiBehaviorOptions(options => options.InvalidModelStateResponseFactory = ModelStateValidator.ValidModelState);
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: "WebSitesPolicy", policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
});
builder.Services.AddControllers();
builder.Services.AddScoped<DapperContext>(); builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IDapperService, DapperService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITaskRepository, TaskRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // var securityScheme = new OpenApiSecurityScheme
    // {
    //     Name = "Authorization",
    //     Type = SecuritySchemeType.ApiKey,
    //     Scheme = "bearer",
    //     BearerFormat = "JWT",
    //     In = ParameterLocation.Header,
    //     Description = "Enter JWT Bearer token **_only_**",
    //     Reference = new OpenApiReference
    //     {
    //         Id = JwtBearerDefaults.AuthenticationScheme,
    //         Type = ReferenceType.SecurityScheme
    //     }
    // };
    // c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    // c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new string[] { } } });

    c.SwaggerDoc("Users", new OpenApiInfo
    {
        Title = "Users",
        Version = "v1",
        Description = "Documento para la administracion de los usuarios",
        Contact = new OpenApiContact
        {
            Name = "Alan Castro",
            Email = "test@test.com",
        },
        License = new OpenApiLicense
        {
            Name = "Use inder license ###"
        }
    });

    c.SwaggerDoc("Tasks", new OpenApiInfo
    {
        Title = "Tasks",
        Version = "v1",
        Description = "Documento para la administracion de las tareas de los usuarios",
        Contact = new OpenApiContact
        {
            Name = "Alan Castro",
            Email = "test@test.com",
        },
        License = new OpenApiLicense
        {
            Name = "Use inder license ###"
        }
    });

});


builder.Services.AddHttpContextAccessor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(c => c.RouteTemplate = "/swagger/{documentname}/swagger.json");

    app.UseSwaggerUI(c =>
    {
        c.ConfigObject = new ConfigObject
        {
            ShowCommonExtensions = true
        };

        c.RoutePrefix = "swagger";
        c.InjectStylesheet("/swagger-ui/custom.css");
        c.InjectJavascript("/swagger-ui/custom.js");
        c.SwaggerEndpoint("Users/swagger.json", "Users");
        c.SwaggerEndpoint("Tasks/swagger.json", "Tasks");
    });
}
else
{
    app.UseSwagger(c => c.RouteTemplate = "/apiDocumentation/{documentname}/swagger.json");

    app.UseSwaggerUI(c =>
    {
        c.ConfigObject = new ConfigObject
        {
            ShowCommonExtensions = true
        };

        c.RoutePrefix = "apiDocumentation";
        c.InjectStylesheet("/swagger-ui/custom.css");
        c.InjectJavascript("/swagger-ui/custom.js");
        c.SwaggerEndpoint("Users/swagger.json", "Users");
        c.SwaggerEndpoint("Tasks/swagger.json", "Tasks");
    });
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("WebSitesPolicy");

app.UseAuthorization();

app.UseStaticFiles();

app.UseMiddleware<JwtMiddleware>();

app.MapControllers();

app.Run();
