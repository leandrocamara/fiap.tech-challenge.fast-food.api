using Adapters.Extensions;
using API.Filters;
using API.HealthChecks;
using Application.Extensions;
using External.Extensions;

var builder = WebApplication.CreateBuilder(args);

#region Configure Services

var configuration = builder.Configuration;

builder.Services.AddCustomHealthChecks();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => {
    c.EnableAnnotations();
});
builder.Services.AddControllers(options =>
{
    options.Filters.Add<TransactionalContextFilter>();
});

builder.Services.AddExternalDependencies(configuration);
builder.Services.AddAdaptersDependencies();
builder.Services.AddApplicationDependencies();

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.DocumentTitle = "FastFood API Documentation - Tech Challenge FIAP";
        c.SpecUrl = "/swagger/v1/swagger.json";
    });

    app.CreateDatabase(configuration);
}

app.UseCustomHealthChecks();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();