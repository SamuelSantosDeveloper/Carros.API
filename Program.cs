using System.Text;
using Carros.Api.Data;
using Carros.Api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

string sqlServer = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(sqlServer));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
     .AddEntityFrameworkStores<ApplicationDbContext>()
     .AddDefaultTokenProviders();


builder.Services.AddScoped<IAuthenticate, AuthenticateService>();

builder.Services.AddSwaggerGen(x =>{
     x.SwaggerDoc("v1", new OpenApiInfo {Title = "Carros.API", Version = "v1"});

     x.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme(){
          Name = "Authorization",
          Type = SecuritySchemeType.ApiKey,
          Scheme = "Bearer",
          BearerFormat = "JWT",
          In = ParameterLocation.Header,
          Description = "JWT Authorization header using the Bearer scheme."
           + " \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below." 
           + "\r\n\r\nExample: \"Bearer 12345abcdef\"",
     });
     x.AddSecurityRequirement(new OpenApiSecurityRequirement
     {
               {
                    new OpenApiSecurityScheme
                    {
                         Reference = new OpenApiReference
                         {
                             Type = ReferenceType.SecurityScheme,
                             Id = "Bearer"
                         }
                    },
                         new string [] {}
               }
     });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddJwtBearer(options =>{
     options.TokenValidationParameters = new TokenValidationParameters{
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
     };
});

builder.Services.AddScoped<ICarroService, CarrosService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
