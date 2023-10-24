using NLog;
using ShipmentService.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);
LogManager.Setup().LoadConfigurationFromFile("nlog.config");
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.ConfigureServicesApplication(builder.Configuration);
builder.Services.ConfigureMassTransit();
var app = builder.Build();
app.Configure();


 
