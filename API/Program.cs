using Persistence;
using Microsoft.EntityFrameworkCore;
using API.Extensions;
using API.Middleware;
using FluentValidation.AspNetCore;
using Application.Users.Commands;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
        options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
    )
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<Edit>());

builder.Services.AddApplicationServices();

var app = builder.Build();

app.UseMiddleware<ExceptionMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
app.UseRouting();
app.UseCors(API.Constants.CORS_POLICY);
app.UseAuthorization();
app.UseEndpoints(endpoints => endpoints.MapControllers());

using var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;

try
{
    var context = services.GetRequiredService<DataContext>();
    await context.Database.MigrateAsync();
    await Seed.SeedDataAsync(context);
}
catch (Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration");
}

await app.RunAsync();

public partial class Program { }