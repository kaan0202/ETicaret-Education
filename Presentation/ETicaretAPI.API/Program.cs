using ETicaretAPI.API.Configuration;
using ETicaretAPI.API.Extensions;
using ETicaretAPI.Application;
using ETicaretAPI.Application.Abstraction.Storage.Local;
using ETicaretAPI.Application.Validators.Products;
using ETicaretAPI.Infrasracture;
using ETicaretAPI.Infrasracture.Filters;
using ETicaretAPI.Infrasracture.Services.Storage.Local;
using ETicaretAPI.Persistance;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.HttpLogging;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Sinks.PostgreSQL;
using System.Reflection;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options => options.Filters.Add<ValidationFilter>())
   .AddFluentValidation(configuration => configuration.RegisterValidatorsFromAssemblyContaining<CreateProductValidator>())
   .ConfigureApiBehaviorOptions(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddPersistanceServices();
builder.Services.AddApplicationServices();


builder.Services.AddInfrastractureServices();
builder.Services.AddStorage<LocalStorage>();
builder.Services.AddCors(options => options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200", "https://localhost:4200").AllowAnyHeader().AllowAnyMethod()));


Logger log = new LoggerConfiguration()
    .WriteTo.Console()
    
    .WriteTo.File("logs/log.txt")
    .WriteTo.PostgreSQL(connectionString: builder.Configuration.GetConnectionString("PostgreSQL"), "logs", needAutoCreateTable: true, columnOptions: new Dictionary<string, ColumnWriterBase>
    {
     { "message", new RenderedMessageColumnWriter()},
     { "message_template", new MessageTemplateColumnWriter() },
     {"level", new LevelColumnWriter() },
     {"time_stamp", new TimestampColumnWriter() },
     {"exception", new ExceptionColumnWriter() },
     {"log_event", new LogEventSerializedColumnWriter()},
     {"user_name", new UsernameColumnWriter() }

    })
    .Enrich.FromLogContext() 
    .WriteTo.Seq("http://localhost:5341/")
    .MinimumLevel.Information()
    .CreateLogger();

builder.Host.UseSerilog(log);
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("sec-ch-ua");
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer("Admin", options => {
    options.TokenValidationParameters = new()
    {
        ValidateAudience = true, //Olu�turulacabilecek token de�erini kimlerin/hangi originlerin/sitelerin kullan�c� belirledi�imiz de�erdir.
        ValidateIssuer = true, //Olu�turulacak token de�erini kimin da��tt���n� ifade edece�imiz aland�r.
        ValidateLifetime = true, //Olu�turulan token de�erini s�resini kontrol edecek olan do�rulamad�r.
        ValidateIssuerSigningKey = true, //�retilecek token de�erinin uygulamam�za ait bir de�er oldu�unu ifade eden security key verisinin do�rulanmas�d�r.

        ValidAudience = builder.Configuration["Token:Audience"],
        ValidIssuer = builder.Configuration["Token:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(builder.Configuration["Token:SecurityKey"])),
        LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires >DateTime.UtcNow : false,
        NameClaimType = ClaimTypes.Name //JWT �zerinde Name claim'ine kar��l�k gelen de�eri User.Identity.Name propertysinden elde edebiliriz.
    };

    
});

var app = builder.Build();
app.UseStaticFiles();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureExceptionHandler<Program>(app.Services.GetRequiredService<ILogger<Program>>());
app.UseStaticFiles();
app.UseSerilogRequestLogging();
app.UseHttpLogging();
app.UseHttpsRedirection();


app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

app.Use( async (context, next) =>
{
    var username = context.User?.Identity?.Name != null || true ? context.User.Identity.Name : null;
    LogContext.PushProperty("user_name", username);
     await next();

});

app.MapControllers();

app.Run();
