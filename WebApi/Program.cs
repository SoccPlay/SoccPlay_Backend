using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using WebApi.Configuration;
using WebApi.Middleware;
using webbookingfootball.Api.WebApi.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var configuration = new ConfigurationBuilder()
    .SetBasePath(builder.Environment.ContentRootPath)
    .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
    .Build();


builder.Services.AddWebAPIService(configuration);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
//builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebRecruitment.WebApi");
});
app.UseRouting();

app.UseHttpsRedirection();
app.UseMiddleware<GlobalExceptionMiddleware>();

app.UseCors();
app.UseAuthentication();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapHealthChecks("/healthchecks");
    endpoints.MapControllers();
});

app.Run();