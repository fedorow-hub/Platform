using Platform.Services;
using Platform.Models;
using Microsoft.EntityFrameworkCore;
using Platform;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.Configure<MessageOptions>(options => {
//    options.CityName = "Albany";
//});

//builder.Services.AddDistributedSqlServerCache(opts =>
//{
//    opts.ConnectionString = builder.Configuration["ConnectionStrings:CacheConnection"];
//    opts.SchemaName = "dbo";
//    opts.TableName = "DataCache";
//});

//builder.Services.AddResponseCaching();

//builder.Services.AddDbContext<CalculationContext>(opts =>
//{
//    opts.UseSqlServer(builder.Configuration["ConnectionStrings:CalcConnection"]);
//    opts.EnableSensitiveDataLogging(true);
//});

//builder.Services.AddTransient<SeedData>();

var app = builder.Build();



//app.MapGet("/location", async (HttpContext context,
// IOptions<MessageOptions> msgOpts) => {
//     MessageOptions opts = msgOpts.Value;
//     await context.Response.WriteAsync($"{opts.CityName}, {opts.CountryName}");
// });

//app.UseResponseCaching();

//app.MapEndpoint<SumEndpoint>("/sum/{count:int=1000000000}");




//app.MapGet("capital/{country}", Capital.Endpoint);
//app.MapGet("population/{city?}", Population.Endpoint)
//    .WithMetadata(new RouteNameMetadata("population"));//population должно в точности совпадать с указанным в 
//                                                       //методе GetPathByRouteValues данного ендпоинта
app.MapGet("population/{city?}", Population.Endpoint);

//app.Map("{number:int}", async context => {
//    await context.Response.WriteAsync("Routed to the int endpoint");
//}).Add(b => ((RouteEndpointBuilder)b).Order = 1);
//app.Map("{number:double}", async context => {
//    await context.Response
//    .WriteAsync("Routed to the double endpoint");
//}).Add(b => ((RouteEndpointBuilder)b).Order = 2);

//app.MapGet("middleware/function", async (HttpContext context, IResponseFormatter formatter) => {
//    await formatter.Format(context,
//    "Middleware Function: It is snowing in Chicago");
//});

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

//app.Run(context =>
//{
//    throw new Exception("Something has gone wrong");
//});

bool cmdLineInit = (app.Configuration["INITDB"] ?? "false") == "true";

if (app.Environment.IsDevelopment() || cmdLineInit)
{
    var seedData = app.Services.GetRequiredService<SeedData>();
    seedData.SeedDatabase();
}
if (!cmdLineInit)
{
    app.Run();
}
