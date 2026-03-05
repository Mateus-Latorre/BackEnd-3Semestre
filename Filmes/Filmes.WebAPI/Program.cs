// Program.cs
using Filmes.WebAPI.BdContextFilme;
using Filmes.WebAPI.Interfaces;
using Filmes.WebAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Metadata;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<FilmeContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IGeneroRepository, GeneroRepository>();
//builder.Services.AddScoped<IFilmeRepository, FilmeRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
//Adiciona serviço de Jwt Bearer (forma de autenticação)

builder.Services.AddAuthentication(options =>
{
options.DefaultChallengeScheme = "JwtBearer";
options.DefaultAuthenticateScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        //valida quem está solicitando o token
        ValidateIssuer = true,
        //valida quem irá receber o token
        ValidateAudience = true,
        //Valida a expiração do token
        ValidateLifetime = true,
        //Chave de acesso do token
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("filmes-chave-autenticacao-webapi-dev")),
        //Tempo de expiração do token
        ClockSkew = TimeSpan.FromMinutes(5),
        //nome do issuer (de onde está vindo)
        ValidIssuer = "api_filmes",
        //nome do audience (para onde ele está indo)
        ValidAudience = "api_filmes"
    };
});

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Filmes API",
        Description = "Uma API com catálogo de filmes",
        TermsOfService = new Uri("https://example.com/terms"),
        Contact = new OpenApiContact
        {
            Name = "Mateus Latorre",
            Url = new Uri("https://github.com/Mateus-Latorre")
        },
        License = new OpenApiLicense {
            Name = "Example License",
        Url = new Uri("https://example.com/license")
        }
    });

    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Insira o token JWT:"
    });

    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("Bearer", document)] = Array.Empty<string>().ToList()
    });

});

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddControllers();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options => { });
    app.UseSwaggerUI(options => {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseStaticFiles();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();