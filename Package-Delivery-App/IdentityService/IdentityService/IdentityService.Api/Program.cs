using IdentityService.Api.Extensions;
using NLog;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile("nlog.config");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureApplicationServices(builder.Configuration);
builder.Services.ConfigureMassTransit();
var app = builder.Build();
app.Configure();

 