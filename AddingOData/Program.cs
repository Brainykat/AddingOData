using AddingOData.Extensions;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddOData(options => 
{
  options.Select().Filter().OrderBy();
  //options.AddRouteComponents("v{version}", edmModel)
}); ;


//builder.Services.AddControllers().AddOData(opt => opt.Select().Count());

builder.Services.TryAddSingleton<IODataModelProvider, MyODataModelProvider>();

builder.Services.TryAddEnumerable(
    ServiceDescriptor.Transient<IApplicationModelProvider, MyODataRoutingApplicationModelProvider>());

builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<MatcherPolicy, MyODataRoutingMatcherPolicy>());

builder.Services.AddSwaggerGen(
    opt => opt.ResolveConflictingActions(a => a.First()));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseDeveloperExceptionPage();
}


app.UseODataRouteDebug(); // Remove it if not needed

app.UseSwagger();
app.UseSwaggerUI(c =>
{
  c.SwaggerEndpoint("/swagger/v1/swagger.json", "OData 8.x OpenAPI");
});

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
  endpoints.MapControllers();
});

app.Run();
