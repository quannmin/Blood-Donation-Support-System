using BloodBE.API;

var builder = WebApplication.CreateBuilder(args);

// config appsettings by env
builder.Configuration
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
    .AddEnvironmentVariables();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();
builder.Services.AddConfig(builder.Configuration);
builder.Services.AddConfigJWT(builder.Configuration);
builder.Services.AddCorsPolicyBackend();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

if (builder.Environment.IsProduction() && builder.Configuration.GetValue<int?>("PORT") is not null)
{
    builder.WebHost.UseUrls($"http://*:{builder.Configuration.GetValue<int>("PORT")}");
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigins");


app.UseAuthorization();
app.UseAuthentication();

app.MapControllers();

app.Run();
