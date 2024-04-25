using DevFreela.Infrastructure.Persistence; // Importa o contexto do banco de dados
using Microsoft.EntityFrameworkCore; // Importa para configura��o do Entity Framework
using Microsoft.OpenApi.Models; // Importa para configura��o do Swagger
using Microsoft.AspNetCore.Builder; // Importa para configura��o do pipeline de middleware
using Microsoft.Extensions.DependencyInjection; // Importa para configura��o de servi�os
using Microsoft.Extensions.Hosting; // Importa para configura��o do ambiente de hospedagem
using MediatR; // Importa para configura��o do Mediator
using DevFreela.Application.Commands.CreateProject; // Importa comando de cria��o de projeto
using DevFreela.Core.Repositories; // Importa reposit�rios principais
using DevFreela.Infrastructure.Persistence.Repositories; // Importa implementa��es de reposit�rios
using FluentValidation; // Importa para valida��o de modelos
using FluentValidation.AspNetCore; // Importa para integra��o com o FluentValidation
using DevFreela.Application.Validators; // Importa validadores de comandos
using DevFreela.API.Filters; // Importa filtro de valida��o global
using DevFreela.Core.Services; // Importa servi�os principais
using Microsoft.AspNetCore.Authentication.JwtBearer; // Importa para autentica��o JWT
using Microsoft.IdentityModel.Tokens; // Importa para configura��o de tokens JWT
using System.Text; // Importa para manipula��o de texto
using System; // Importa para uso geral
using System.IO;
using Microsoft.Extensions.Configuration;
using DevFreela.Infrastructure.Auth; // Importa para manipula��o de arquivos

var builder = WebApplication.CreateBuilder(args);

// Carregando as configura��es do arquivo appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Configura��o do banco de dados
builder.Services.AddDbContext<DevFreelaDbContext>(options =>
    options.UseSqlServer(configuration.GetConnectionString("DevFreelaCs")));

// Registro de reposit�rios
builder.Services.AddScoped<IProjectRepository, ProjectRepository>(); // Registra o reposit�rio de projetos
builder.Services.AddScoped<IUserRepository, UserRepository>(); // Registra o reposit�rio de usu�rios
builder.Services.AddScoped<ISkillRepository, SkillRepository>(); // Registra o reposit�rio de habilidades
builder.Services.AddScoped<IAuthService, AuthService>(); // Registra o servi�o de autentica��o 

// Adiciona um filtro de valida��o global a todos os controladores da aplica��o.
builder.Services.AddControllers(options => options.Filters.Add(typeof(ValidationFilter)));

// Habilita a valida��o autom�tica do FluentValidation no ASP.NET Core.
builder.Services.AddFluentValidationAutoValidation();

// Adiciona adaptadores para a valida��o do lado do cliente usando FluentValidation.
builder.Services.AddFluentValidationClientsideAdapters();

// Adiciona validadores da mesma assembly que cont�m os validadores para os comandos CreateUser e CreateProject.
builder.Services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CreateProjectCommandValidator>();

// Adiciona servi�os para controladores
builder.Services.AddControllers();

// Adiciona servi�os para explorador de API
builder.Services.AddEndpointsApiExplorer();


// Mediator
builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(typeof(CreateProjectCommand).Assembly); });
// Configura o Mediator



// Configura��o do Swagger
builder.Services.AddSwaggerGen(option =>
{
    // Configura��es b�sicas do Swagger
    option.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "DevFreela.API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            // Contatos
            Name = "LSdev",
            Email = "larasantanadev@gmail.com"
        }
    });




    // Configura��o de seguran�a JWT para o Swagger
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization", // Nome do par�metro de autoriza��o
        Type = SecuritySchemeType.ApiKey, // Tipo do esquema de seguran�a (API key)
        Scheme = "Bearer", // Esquema de autentica��o (Bearer)
        BearerFormat = "JWT", // Formato do token (JWT)
        In = ParameterLocation.Header, // Localiza��o do token (no cabe�alho)
        Description = "JWT Authorization header usando o esquema Bearer." // Descri��o do esquema de autentica��o
    });




    // Adiciona requisito de seguran�a para todas as opera��es do Swagger
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
{
    {
        // Define o esquema de seguran�a a ser utilizado
        new OpenApiSecurityScheme
        {
            // Define uma refer�ncia ao esquema de seguran�a anteriormente definido
            Reference = new OpenApiReference
            {
                // Especifica o tipo de refer�ncia como SecurityScheme
                Type = ReferenceType.SecurityScheme,
                // Identifica o esquema de seguran�a a ser utilizado, que neste caso � "Bearer"
                Id = "Bearer"
            }
        },
        // Define as permiss�es necess�rias para acessar as opera��es protegidas pelo esquema de seguran�a
        new string[] {}  // Recebe uma string vazia
    }
});



    // Incluindo os coment�rios XML para documenta��o do Swagger
    var xmlFile = "DevFreela.API.xml"; // Nome do arquivo XML de documenta��o
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); // Caminho completo do arquivo XML
    option.IncludeXmlComments(xmlPath); // Inclui os coment�rios XML na documenta��o do Swagger
});




// Configura��o de AUTENTICA��O JWT
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme) // Define o esquema de autentica��o padr�o como JWT Bearer
    .AddJwtBearer(options => // Configura o JWT Bearer
    {
        options.TokenValidationParameters = new TokenValidationParameters // Define os par�metros de valida��o do token JWT
        {
            ValidateIssuer = true, // Indica se deve validar o emissor (issuer) do token
            ValidateAudience = true, // Indica se deve validar o destinat�rio (audience) do token
            ValidateLifetime = true, // Indica se deve validar a validade do token
            ValidateIssuerSigningKey = true, // Indica se deve validar a chave de assinatura do emissor (issuer) do token


            ValidIssuer = configuration["Jwt:Issuer"], // Define o emissor (issuer) v�lido do token, lendo do arquivo de configura��o
            ValidAudience = configuration["Jwt:Audience"], // Define o destinat�rio (audience) v�lido do token, lendo do arquivo de configura��o
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"])) // Define a chave de assinatura v�lida para verificar a autenticidade do token, lendo do arquivo de configura��o
        };
    });






var app = builder.Build();

// Configure o pipeline de solicita��o HTTP.
if (app.Environment.IsDevelopment())
{
    // Habilitando o Swagger para ambiente de desenvolvimento
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middleware para redirecionamento HTTPS e autoriza��o
app.UseHttpsRedirection();


// Autentica��o para processar e validar os tokens de autentica��o recebidos nas requisi��es
app.UseAuthentication();
// Autoriza��o para validar as pol�ticas de autoriza��o definidas nos endpoints da aplica��o
app.UseAuthorization();



// Mapeamento de controladores
app.MapControllers();

// Inicia a aplica��o
app.Run();
