using Customer_Management.Application;
using Customer_Management.Identity;
using Customer_Management.Persistence;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
//
//builder.Services.AddSwaggerGen();

AddSwagger(builder.Services);
builder.Services.ConfigureApplicationServicesRegistration();
builder.Services.ConfigurePersistenceServices(builder.Configuration);
builder.Services.ConfigureIdentityServices(builder.Configuration);

builder.Services.AddCors(o =>
{
    o.AddPolicy("CorsPolicy", b =>
    b.AllowAnyOrigin()
    .AllowAnyMethod()
    .AllowAnyHeader()
    );


});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseAuthentication();
app.UseHttpsRedirection();

app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();

app.Run();

void AddSwagger(IServiceCollection services)
{
    builder.Services.AddSwaggerGen(o =>
    {
        o.AddSecurityDefinition(name: "Bearer", new OpenApiSecurityScheme()
        {
            Description = "@JWT Authorization header using the Bearer scheme." +
            "Enter 'Bearer' [space] and than your token in the next input below." +
            "Example: 'Bearer 123qweer'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });
        o.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                 new OpenApiSecurityScheme()
                 {
                    Reference = new OpenApiReference()
                    {
                       Type= ReferenceType.SecurityScheme,
                       Id="Bearer"
                    },
                    Scheme ="oauth",
                    Name="Bearer",
                    In= ParameterLocation.Header
                 },
                new List<string>()
            }
            
        });
        o.SwaggerDoc("v1",new OpenApiInfo() {
            Version = "v1",
            Title="Customer Mangament Api"
        });


    });
}