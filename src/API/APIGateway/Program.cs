using APIGateway.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;
using Yarp.ReverseProxy.Transforms;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"))
    .AddTransforms(builderContext =>
    {
        builderContext.AddRequestHeader("X-Forwarded-Host", "ApiGateway");
    });

var jwtOptions = new JWTOptions();
builder.Configuration.Bind("JWT", jwtOptions);
var authenticationOptions = new AuthenticationOptions();
builder.Configuration.Bind("Authentication", authenticationOptions);
var authenticationClientOptions = new AuthenticationClientOptions();
builder.Configuration.Bind("Authentication:Client", authenticationClientOptions);
var audiences = authenticationClientOptions.Audiences.Split(',');
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = authenticationOptions.Authority;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            ValidateIssuer = true,
            ValidIssuer = authenticationOptions.Issuer,
            ValidateAudience = true,
            ValidAudiences = audiences,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Key)),
            ClockSkew = TimeSpan.Zero
        };
        // For Dev env can be disabled. In Prod, it should be enabled
        options.RequireHttpsMetadata = false;
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "GatewayAPI",
        Version = "v1",
    });
});

var app = builder.Build();

app.MapReverseProxy();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
