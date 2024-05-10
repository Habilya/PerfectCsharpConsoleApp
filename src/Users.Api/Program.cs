using Microsoft.Net.Http.Headers;
using Users.Api.Data;
using Users.Api.Logging;
using Users.Api.Options;
using Users.Api.Repositories;
using Users.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = builder.Configuration;
var dbConnectionOptionsSection = config.GetSection(nameof(DbConnectionOptions));
builder.Services.Configure<DbConnectionOptions>(dbConnectionOptionsSection);
builder.Services.AddSingleton<ISqliteDbConnectionFactory, SqliteDbConnectionFactory>();
builder.Services.AddSingleton<DatabaseInitializer>();
builder.Services.AddSingleton<IUserRepository, UserRepository>();
builder.Services.AddSingleton<IUserService, UserService>();
builder.Services.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));

builder.Services.AddSingleton<ITypicodeService, TypicodeService>();
builder.Services.AddHttpClient("TypicodeApi", httpClient =>
{
	httpClient.BaseAddress = new Uri(config.GetValue<string>("TypicodeApi:ApiBaseUrl"));
	httpClient.DefaultRequestHeaders.Add(
		HeaderNames.Accept, "application/json");
});

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

var dbInitializer = app.Services.GetRequiredService<DatabaseInitializer>();
await dbInitializer.InitializeAsync();

app.Run();
